using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DoAnHQTCSDL.Startup))]
namespace DoAnHQTCSDL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
