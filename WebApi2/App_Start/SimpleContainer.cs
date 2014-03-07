using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using WebApi2.Controllers;
using WebApi2.Models;

namespace WebApi2.App_Start
{
    public class SimpleContainer : IDependencyResolver
    {
        static readonly IProductRepository Repository = new ProductRepository();
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (ProductsController))
            {
                return new ProductsController(Repository);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}