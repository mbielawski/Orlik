using Orlik.Web.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace Orlik.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            UnityWebActivator.Start();
            AutoMapperConfig.Initialize();
        }
    }
}
