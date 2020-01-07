using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrelloUtilities.Models;

namespace TrelloWebApplication.Controllers
{
    public class ApiModelsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: ApiModels
        public ActionResult Index()
        {
            return View(db.ApiModels.ToList());
        }

        // GET: ApiModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiModel apiModel = db.ApiModels.Find(id);
            if (apiModel == null)
            {
                return HttpNotFound();
            }
            return View(apiModel);
        }

        // GET: ApiModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBoard,Token,Key")] ApiModel apiModel)
        {
            if (ModelState.IsValid)
            {
                db.ApiModels.Add(apiModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apiModel);
        }

        // GET: ApiModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiModel apiModel = db.ApiModels.Find(id);
            if (apiModel == null)
            {
                return HttpNotFound();
            }
            return View(apiModel);
        }

        // POST: ApiModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBoard,Token,Key")] ApiModel apiModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apiModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apiModel);
        }

        // GET: ApiModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApiModel apiModel = db.ApiModels.Find(id);
            if (apiModel == null)
            {
                return HttpNotFound();
            }
            return View(apiModel);
        }

        // POST: ApiModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApiModel apiModel = db.ApiModels.Find(id);
            db.ApiModels.Remove(apiModel);
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
