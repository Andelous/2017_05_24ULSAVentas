using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_2017_05_24ULSAVentas.Startup))]
namespace _2017_05_24ULSAVentas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
