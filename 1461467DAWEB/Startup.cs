using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1461467DAWEB.Startup))]
namespace _1461467DAWEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
