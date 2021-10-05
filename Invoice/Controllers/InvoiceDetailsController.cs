using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Invoice.Models;

namespace Invoice.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers:"*",methods:"*")]

    public class InvoiceDetailsController : ApiController
    {
        private DB_InvoiceEntities db = new DB_InvoiceEntities();

        // GET: api/InvoiceDetails
        public IQueryable<InvoiceDetail> GetInvoiceDetails()
        {
            return db.InvoiceDetails;
        }

        // GET: api/InvoiceDetails/5
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult GetInvoiceDetail(int id)
        {
            var  invoiceDetail = db.InvoiceDetails.Where(e=>e.InvoiceID==id).ToList();
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return Ok(invoiceDetail);
        }

        [HttpGet]
        [Route("api/InvoiceDetails/ProductList")]
        public IQueryable<Product> ProductList()
        {
            return db.Products;
        }


        // PUT: api/InvoiceDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoiceDetail(int id, InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoiceDetail.InvoiceDetailID)
            {
                return BadRequest();
            }

            db.Entry(invoiceDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/InvoiceDetails
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult PostInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InvoiceDetails.Add(invoiceDetail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoiceDetail.InvoiceDetailID }, invoiceDetail);
        }


      
        [ResponseType(typeof(InvoiceDetail))]
        public IHttpActionResult DeleteInvoiceDetail(int id ,int i)
        {
            InvoiceDetail invoiceDetail = db.InvoiceDetails.Where(e=>e.InvoiceID==id &&e.ProductID == i ).FirstOrDefault();
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            db.InvoiceDetails.Remove(invoiceDetail);
            db.SaveChanges();

            return Ok(invoiceDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceDetailExists(int id)
        {
            return db.InvoiceDetails.Count(e => e.InvoiceDetailID == id) > 0;
        }
    }
}