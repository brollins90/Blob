using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blob.Proxies;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Before.DummyOwinAuth
{
    // One instance is created when the application starts.
    public class DummyAuthMiddleware
    {
        public const string Key = "idm:DummyAuthMiddleware";

        private readonly Func<IDictionary<string, object>, Task> _next;
        private DummyAuthenticationOptions _options;

        public DummyAuthMiddleware(Func<IDictionary<string, object>, Task> next, DummyAuthenticationOptions options)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            env[Key] = new BeforeAuthorizationClient("","","")
            await _next(env);
        }
    }
}
