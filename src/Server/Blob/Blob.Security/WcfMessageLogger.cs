using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using log4net;

namespace Blob.Security
{
    public class WcfMessageLogger : IDispatchMessageInspector, IServiceBehavior
    {
        private readonly ILog _log;

        public WcfMessageLogger()
        {
            _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        #region IDispatchMessageInspector

        public object AfterReceiveRequest(ref Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            if (request != null)
            {
                _log.Debug("\n" + request);
                //Debug.Write(request.ToString());
            }
            else
            {
                _log.Debug("AfterReceiveRequest was called, but the request was null.");
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (reply != null)
            {
                _log.Debug("\n" + reply);
            }
            else
            {
                _log.Debug("BeforeSendReply was called, but the reply was null.");
            }
        }

        #endregion

        #region IServiceBehavior

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new WcfMessageLogger());
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
        }

        #endregion
    }
}
