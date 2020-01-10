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
            return View(db.ApiModels.OrderBy(g=>g.Primo).ToList());
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
                if (db.ApiModels.Count()>0)
                {
                    apiModel.Primo = 100;
                }
                else
                {
                    apiModel.Primo = 0;
                }               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apiModel);
        }
        public ActionResult Primo(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var apiCred in db.ApiModels.ToList())
            {
                if (apiCred.Primo==0)
                {
                    apiCred.Primo = 100;
                    
                }
                if (apiCred.IdBoard==id)
                {
                    apiCred.Primo = 0;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
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
