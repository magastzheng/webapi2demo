using System.Web.Http;
using Microsoft.Practices.Unity;
using WebApi2.App_Start;
using WebApi2.Controllers;
using WebApi2.Models;

namespace WebApi2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private void ConfigureApi(HttpConfiguration config)
        {
            //config.DependencyResolver = new SimpleContainer();
            var unity = new UnityContainer();
            unity.RegisterType<ProductsController>();
            unity.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
        }

        protected void Application_Start()
        {
            ConfigureApi(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new NotImplExceptionFilterAttribute());
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.UseDataContractJsonSerializer
        }
    }
}
