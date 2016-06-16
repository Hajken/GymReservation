using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymReservation.Startup))]
namespace GymReservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
