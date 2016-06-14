using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoteTaker.Startup))]
namespace NoteTaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
