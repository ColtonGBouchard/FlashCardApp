using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlashCardApp.Startup))]
namespace FlashCardApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
