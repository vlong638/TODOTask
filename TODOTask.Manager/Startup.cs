using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TODOTask.Manager.Startup))]
namespace TODOTask.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
