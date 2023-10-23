using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MktAcademy.Startup))]
namespace MktAcademy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
