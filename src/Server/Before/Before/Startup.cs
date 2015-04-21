using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Before.Startup))]
namespace Before
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
