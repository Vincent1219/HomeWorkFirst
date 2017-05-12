using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeWorkfirst.Startup))]
namespace HomeWorkfirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
