﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        public IHttpActionResult GetProduct(int id)
        {
            throw new NotImplementedException("This method is not implemented!");
            var product = repository.Get(id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(product);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository.GetAll().Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProduct(Product item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new {id = item.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduct(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
