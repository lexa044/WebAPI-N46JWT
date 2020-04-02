using System.Web.Http;

namespace DNKit.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            UnityConfig.RegisterComponents();
            //AutoMapperConfig.Configure();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
