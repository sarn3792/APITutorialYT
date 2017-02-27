using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestingCrossDomain.Startup))]
namespace TestingCrossDomain
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
