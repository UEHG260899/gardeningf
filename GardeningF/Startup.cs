using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GardeningF.Startup))]
namespace GardeningF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
