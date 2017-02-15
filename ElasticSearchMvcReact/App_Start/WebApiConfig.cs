using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ElasticSearchMvcReact.BL.Providers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;
using WebAPIRestWithNest.App_Start;

namespace WebAPIRestWithNest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());

            WebApiUnityActionFilterProvider.RegisterFilterProviders(config);
        }
    }
}
