using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Сountry_cottage_area.Startup))]
namespace Сountry_cottage_area
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
