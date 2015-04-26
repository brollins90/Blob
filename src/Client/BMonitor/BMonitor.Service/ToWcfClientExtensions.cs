using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.DynamicProxy;
using Ninject;
using Ninject.Syntax;

namespace BMonitor.Service
{
    // Modified from examples at luisfsgoncalves.wordpress.com/2012/02/28/mixin-up-ninject-castle-dynamic-proxy-and-wcf-part-iii/ 
    // to use a channel factory cache.
    public static class ToWcfClientExtensions
    {
        public static IBindingWhenInNamedWithOrOnSyntax<T> ToWcfClient<T>(this IBindingToSyntax<T> syntax) where T : class
        {
            return syntax.ToMethod(ctx => ctx.Kernel
                .Get<ProxyGenerator>()
                .CreateInterfaceProxyWithoutTarget<T>(new WcfProxyWithDisposalInterceptor<T>()));
        }
    }

    public class WcfProxyWithDisposalInterceptor<TInterface> : IInterceptor 
        where TInterface : class
    {
        void IInterceptor.Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.Equals("Dispose", StringComparison.Ordinal))
            {
                throw new InvalidOperationException("Dispose invoked on WcfProxyWithDisposalInterceptor");
            }

            // I use a global ChannelFactory cache instead of caching using the Configuration System.
            using (var channel = (IDisposable)ChannelFactoryCache.CreateChannel<TInterface>())
            {
                invocation.ReturnValue = InvokeMethod(invocation, channel, invocation.Arguments);
            }
        }

        private static object InvokeMethod(IInvocation invocation, object channel, object[] arguments)
        {
            try
            {
                return invocation.Method.Invoke(channel, arguments);
            }
            catch (TargetInvocationException ex)
            {
                // Preserve stack trace
                var stackTrace = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.Instance | BindingFlags.NonPublic);
                if (stackTrace != null)
                {
                    stackTrace.SetValue(ex.InnerException, ex.InnerException.StackTrace + Environment.NewLine);
                }
                throw ex.InnerException;
            }
        }
    }

    public static class ChannelFactoryCache
    {
        private static Dictionary<Type, Tuple<EndpointAddress, object>> cache = new Dictionary<Type, Tuple<EndpointAddress, object>>();

        public static void Add<TInterface>(Uri uri, Binding binding, List<IEndpointBehavior> behaviors)
        {
            var endpointAddress = new EndpointAddress(uri);

            var factory = new ChannelFactory<TInterface>("*");

            if (behaviors != null)
            {
                foreach (var behavior in behaviors)
                {
                    factory.Endpoint.Behaviors.Add(behavior);
                }
            }

            if (!cache.ContainsKey(typeof(TInterface)))
            {
                cache.Add(typeof(TInterface), new Tuple<EndpointAddress, object>(endpointAddress, factory));
            }
        }

        public static void Add<TInterface>(string endpointConfigurationName)
        {
            ClientSection clientSection = (ClientSection)WebConfigurationManager.GetSection("system.serviceModel/client");
            ChannelEndpointElement endpoint = clientSection.Endpoints[0];

            Uri uri = new Uri(endpoint.Address.ToString());
            var endpointAddress = new EndpointAddress(uri);

            var factory = new ChannelFactory<TInterface>(endpoint.Name);
            
            if (!cache.ContainsKey(typeof(TInterface)))
            {
                cache.Add(typeof(TInterface), new Tuple<EndpointAddress, object>(endpointAddress, factory));
            }
        }

        public static TInterface CreateChannel<TInterface>() 
            where TInterface : class
        {
            var data = cache[typeof(TInterface)];

            return (data.Item2 as ChannelFactory<TInterface>).CreateChannel(data.Item1) as TInterface;
        }
    }
}
