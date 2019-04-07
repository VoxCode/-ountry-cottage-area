using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Сountry_cottage_area.Models;

namespace Сountry_cottage_area.Controllers
{
    public class AgriculturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Agricultures
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Agricultures.ToList());
        }

        // GET: Agricultures/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agriculture agriculture = db.Agricultures.Find(id);
            if (agriculture == null)
            {
                return HttpNotFound();
            }
            return View(agriculture);
        }

        // GET: Agricultures/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agricultures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Variable,AgricultureIndex,AgricultureNumber,Date,AreaId")] Agriculture agriculture)
        {
            if (ModelState.IsValid)
            {
                db.Agricultures.Add(agriculture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agriculture);
        }

        // GET: Agricultures/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agriculture agriculture = db.Agricultures.Find(id);
            if (agriculture == null)
            {
                return HttpNotFound();
            }
            return View(agriculture);
        }

        // POST: Agricultures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Variable,AgricultureIndex,AgricultureNumber,Date,AreaId")] Agriculture agriculture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agriculture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agriculture);
        }

        // GET: Agricultures/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agriculture agriculture = db.Agricultures.Find(id);
            if (agriculture == null)
            {
                return HttpNotFound();
            }
            return View(agriculture);
        }

        // POST: Agricultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Agriculture agriculture = db.Agricultures.Find(id);
            db.Agricultures.Remove(agriculture);
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
