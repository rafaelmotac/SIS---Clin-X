using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RMC.TCC.Clinica.Startup))]
namespace RMC.TCC.Clinica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
