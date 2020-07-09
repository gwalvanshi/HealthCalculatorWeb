using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthCalculator.Web.Startup))]
namespace HealthCalculator.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
