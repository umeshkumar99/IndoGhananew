using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IndoGhana.Startup))]
namespace IndoGhana
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
