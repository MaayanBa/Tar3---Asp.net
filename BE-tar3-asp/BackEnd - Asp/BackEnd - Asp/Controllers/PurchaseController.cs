using BackEnd___Asp.DTO;
using CafetriaDBLibrary___Asp.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackEnd___Asp.Controllers
{
    public class PurchaseController : ApiController
    {
        // GET api/<controller>/5
        public List<PurchaseDTO> Get(int id)
        {
            CafeteriaDbContext db = new CafeteriaDbContext();
                List<PurchaseDTO> purchases = db.Purchases.Where(purchase => purchase.StudentId == id).Select(p => new PurchaseDTO()
                {
                    SerialNumber = p.SerialNumber,
                    StudentId = id,
                    Amount = p.Amount,
                    Date = p.Date
                }).ToList();
                return purchases;
        }


        // POST api/<controller>
        public IHttpActionResult Post([FromBody]List<PurchaseDTO> purchases)

        {
            CafeteriaDbContext db = new CafeteriaDbContext();
            try
            {
                foreach (PurchaseDTO p in purchases)
                {
                    Product product = db.Products.Where(prod => prod.SerialNumber == p.SerialNumber).FirstOrDefault();
                    //int inven = Convert.ToInt32(db.Products.Where(t => t.SerialNumber == obj.SerialNumber).Select(x => x.Inventory));

                    if (product.Inventory < p.Amount)
                        return Content(HttpStatusCode.BadRequest, "The amount you are trying to buy is bigger the Inventory");
                    else
                        product.Inventory -= p.Amount;

                    Purchase newPurchase = new Purchase()
                    {
                        StudentId = p.StudentId,
                        SerialNumber = p.SerialNumber,
                        Amount = p.Amount,
                        Date = DateTime.Now
                    };
                    db.Purchases.Add(newPurchase);
                }
                db.SaveChanges();
                if(purchases.Count>0)
                    return Content(HttpStatusCode.OK, "Enjoy your product");
                return Content(HttpStatusCode.OK, "");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var ctx = ((IObjectContextAdapter)db).ObjectContext;
                foreach (var et in ex.Entries)
                {
                    //client win
                    //ctx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.ClientWins, et.Entity);

                    //Store Win
                    ctx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, et.Entity);
                }
                db.SaveChanges();
                return Post(purchases);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}