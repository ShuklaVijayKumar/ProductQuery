using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductQuery.DAL;
using ProductQuery.Models;
using System.Data.Entity.Infrastructure;
using System.Net.Mail;

namespace ProductQuery.Controllers
{
    public class QueryController : Controller
    {
        private ProductQueryContext db = new ProductQueryContext();

        // GET: query
        public ActionResult Index(int? SelectedProduct)
        {
            var products = db.Product.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedProduct = new SelectList(products, "ProductID", "Name", SelectedProduct);
            int productID = SelectedProduct.GetValueOrDefault();

            IQueryable<Query> queries = db.Queries
                .Where(c => !SelectedProduct.HasValue || c.ProductID == productID)
                .OrderBy(d => d.QueryID)
                .Include(d => d.Product);
            var sql = queries.ToString();
            return View(queries.ToList());
        }

        // GET: query/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }


        public ActionResult Create()
        {
            PopulateProductsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QueryID,FirstName,LastName,Email,AddressLine1,City,Postcode,Country,Phone,ProductID")]Query query)
        {
            try
            {
                if (ModelState.IsValid)
                {
//Can be formated well as per the mail content.
                    MailMessage mail = new MailMessage();
                    mail.To.Add("shukla.vijay@outllok.com");
                    mail.From = new MailAddress("tovijay.shukla@gmail.com");
                    mail.Subject = "Query Received";
                    string Body = "Mail Body";
                    mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential
                    ("username", "password");// Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    query.QueryID = db.Queries.Count() + 1;
                    db.Queries.Add(query);
                    
		
	 db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateProductsDropDownList(query.ProductID);
            return View(query);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            PopulateProductsDropDownList(query.ProductID);
            return View(query);
        }

        private void PopulateProductsDropDownList(object selectedProduct = null)
        {
            var productsQuery = from d in db.Product
                                   orderby d.Name
                                   select d;
            ViewBag.ProductID = new SelectList(productsQuery, "ProductID", "Name", selectedProduct);
        }


        // GET: query/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Query query = db.Queries.Find(id);
            if (query == null)
            {
                return HttpNotFound();
            }
            return View(query);
        }

        // POST: query/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Query query = db.Queries.Find(id);
            db.Queries.Remove(query);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
