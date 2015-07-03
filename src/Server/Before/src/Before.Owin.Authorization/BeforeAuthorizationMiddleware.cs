namespace Before.Owin.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BeforeAuthorizationMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly BeforeAuthorizationMiddlewareOptions _options;

        public BeforeAuthorizationMiddleware(Func<IDictionary<string, object>, Task> next, BeforeAuthorizationMiddlewareOptions options)
        {
            _options = options;
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            env[BeforeAuthorizationConstants.Key] = _options.Manager ?? _options.ManagerProvider(env);
            await _next(env).ConfigureAwait(false);
        }
    }
}