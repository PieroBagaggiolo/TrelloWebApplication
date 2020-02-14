using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrelloUtilities.Models;
using TrelloWebApplication.Utiliti;

namespace TrelloWebApplication.Controllers
{
    public class TracingController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        PopolateModel popMod = new PopolateModel();
        // GET: Tracing
        public ActionResult Index()
        {
            Api myApi = popMod.Crea();
            var tracings = db.Tracings.ToList().Where(g=>g.FKboardID==myApi.idBrod);
            return View(tracings);
        }

        // GET: Tracing/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracing tracing = db.Tracings.Find(id);
            if (tracing == null)
            {
                return HttpNotFound();
            }
            return View(tracing);
        }

        // GET: Tracing/Create
        public ActionResult Create()
        {
            ViewBag.FKboardID = new SelectList(db.ApiModels, "IdBoard", "IdBoard");
            return View();
        }

        // POST: Tracing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FKboardID,Event")] Tracing tracing)
        {
            if (ModelState.IsValid)
            {
                db.Tracings.Add(tracing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FKboardID = new SelectList(db.ApiModels, "IdBoard", "IdBoard", tracing.FKboardID);
            return View(tracing);
        }

        // GET: Tracing/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracing tracing = db.Tracings.Find(id);
            if (tracing == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKboardID = new SelectList(db.ApiModels, "IdBoard", "IdBoard", tracing.FKboardID);
            return View(tracing);
        }

        // POST: Tracing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FKboardID,Event")] Tracing tracing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tracing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FKboardID = new SelectList(db.ApiModels, "IdBoard", "IdBoard", tracing.FKboardID);
            return View(tracing);
        }

        // GET: Tracing/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tracing tracing = db.Tracings.Find(id);
            if (tracing == null)
            {
                return HttpNotFound();
            }
            return View(tracing);
        }

        // POST: Tracing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tracing tracing = db.Tracings.Find(id);
            db.Tracings.Remove(tracing);
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
