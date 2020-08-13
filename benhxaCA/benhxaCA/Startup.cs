using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(benhxaCA.Startup))]
namespace benhxaCA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
