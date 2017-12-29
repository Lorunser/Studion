using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Studion.Startup))]
namespace Studion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
