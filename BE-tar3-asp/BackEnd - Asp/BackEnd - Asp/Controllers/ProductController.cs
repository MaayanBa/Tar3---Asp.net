using BackEnd___Asp.DTO;
using CafetriaDBLibrary___Asp.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd___Asp.Controllers
{
    public class ProductController : ApiController
    {
        // GET api/<controller>
        public List<ProductDTO> Get()
        {
            CafeteriaDbContext db = new CafeteriaDbContext();
            var products = db.Products.Select(p => new ProductDTO()
            {
                Name = p.Name,
                SerialNumber = p.SerialNumber,
                Price = p.Price,
                Cost = p.Cost,
                Inventory = p.Inventory,
                SupplierId = p.SupplierId
            }).ToList();
            return products;
        }
    }
}