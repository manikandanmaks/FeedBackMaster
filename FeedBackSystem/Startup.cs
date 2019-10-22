using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeedBackSystem.Startup))]
namespace FeedBackSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
