using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mvc5_ImageGallery.Startup))]
namespace Mvc5_ImageGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
