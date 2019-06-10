using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DodamGroceryMVC.Startup))]
namespace DodamGroceryMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
