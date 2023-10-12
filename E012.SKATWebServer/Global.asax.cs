using System.Web.Http;
using System.Web.Mvc;

namespace E012.SKAT.WebServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
          
        }
    }
}
