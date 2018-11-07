using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Vidly_not_core.Startup))]
namespace Vidly_not_core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
