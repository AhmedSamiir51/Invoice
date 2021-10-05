using Invoice.Models;
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

namespace Invoice.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    
    public class InvoiceController : ApiController
    {
        private DB_InvoiceEntities db = new DB_InvoiceEntities();

        // GET: api/Invoice
        public IQueryable<Invoice.Models.Invoice> GetInvoices()
        {
            return db.Invoices;
        }

        // GET: api/InvoicesData/5
        [ResponseType(typeof(Invoice.Models.Invoice))]
        public IHttpActionResult GetInvoice(int id)
        {
            Invoice.Models.Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/InvoicesData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInvoice(int id, Invoice.Models.Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.InvoiceID)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/InvoicesData
        [ResponseType(typeof(Invoice.Models.Invoice))]
        public IHttpActionResult PostInvoice(Invoice.Models.Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoices.Add(invoice);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = invoice.InvoiceID }, invoice);
        }

        // DELETE: api/InvoicesData/5
        [ResponseType(typeof(Invoice.Models.Invoice))]
        public IHttpActionResult DeleteInvoice(int id)
        {
            Invoice.Models.Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoices.Remove(invoice);
            db.SaveChanges();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(int id)
        {
            return db.Invoices.Count(e => e.InvoiceID == id) > 0;
        }
    }
}