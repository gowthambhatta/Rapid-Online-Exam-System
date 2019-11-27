using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(onllineexam.Startup))]
namespace onllineexam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
