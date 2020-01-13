using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrelloUtilities;
using TrelloUtilities.Models;
using TrelloUtilities.Utility;
using TrelloWebApplication.Utiliti;

namespace TrelloWebApplication.Controllers
{
    public class EmailsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Emails
        public ActionResult Index()
        {
            return View(db.Emails.ToList());
        }



        public ActionResult CreateAdd()
        {
            Email temp = new Email();
            foreach (var item in db.Emails)
            {
                  temp = item;
                  break;
            }
            return View(temp);
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdd([Bind(Include = "SenderEmail,Password,ReceiverEmail")] Email email)
        {
            if (ModelState.IsValid)
            {
                db.Emails.Add(email);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(email);
        }

        // GET: Emails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SenderEmail,Password,ReceiverEmail")] Email email)
        {
            if (ModelState.IsValid)
            {
                string criptate = SecurityPWD.Encrypt(email.Password);
                email.Password = criptate;
                db.Emails.Add(email);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(email);
        }

       

        // GET: Emails/Delete/5
        public ActionResult Delete(int id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return HttpNotFound();
            }
            return View(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Email email = db.Emails.Find(id);
            db.Emails.Remove(email);
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
