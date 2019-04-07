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
    public class AllMapsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AllMaps
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.AllMaps.ToList());
        }

        // GET: AllMaps/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllMap allMap = db.AllMaps.Find(id);
            if (allMap == null)
            {
                return HttpNotFound();
            }
            return View(allMap);
        }

        // GET: AllMaps/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllMaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Cordinates,SaveNumber,AreaId")] AllMap allMap)
        {
            if (ModelState.IsValid)
            {
                db.AllMaps.Add(allMap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allMap);
        }

        // GET: AllMaps/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllMap allMap = db.AllMaps.Find(id);
            if (allMap == null)
            {
                return HttpNotFound();
            }
            return View(allMap);
        }

        // POST: AllMaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Cordinates,SaveNumber,AreaId")] AllMap allMap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allMap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allMap);
        }

        // GET: AllMaps/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllMap allMap = db.AllMaps.Find(id);
            if (allMap == null)
            {
                return HttpNotFound();
            }
            return View(allMap);
        }

        // POST: AllMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            AllMap allMap = db.AllMaps.Find(id);
            db.AllMaps.Remove(allMap);
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
