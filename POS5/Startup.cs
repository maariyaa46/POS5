using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POS5.Startup))]
namespace POS5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
