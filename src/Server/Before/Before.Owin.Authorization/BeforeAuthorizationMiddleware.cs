using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Before.Owin.Authorization
{
    public class BeforeAuthorizationMiddleware
    {
        public const string KEY = "idm:BeforeAuthorizationMiddleware";

        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly BeforeAuthorizationMiddlewareOptions _options;

        public BeforeAuthorizationMiddleware(Func<IDictionary<string, object>, Task> next, BeforeAuthorizationMiddlewareOptions options)
        {
            _options = options;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            env[KEY] = _options.Manager ?? _options.ManagerProvider(env);
            await _next(env).ConfigureAwait(false);
        }
    }
}
