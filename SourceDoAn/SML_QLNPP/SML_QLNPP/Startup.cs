using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SML_QLNPP.Startup))]
namespace SML_QLNPP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
