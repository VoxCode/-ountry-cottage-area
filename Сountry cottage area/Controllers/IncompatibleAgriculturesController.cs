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
    public class IncompatibleAgriculturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncompatibleAgricultures
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var incompatibleAgricultures = db.IncompatibleAgricultures.Include(i => i.AgricultureType);
            return View(incompatibleAgricultures.ToList());
        }

        // GET: IncompatibleAgricultures/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompatibleAgriculture incompatibleAgriculture = db.IncompatibleAgricultures.Find(id);
            if (incompatibleAgriculture == null)
            {
                return HttpNotFound();
            }
            return View(incompatibleAgriculture);
        }

        // GET: IncompatibleAgricultures/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.AgricultureTypeId = new SelectList(db.AgricultureTypes, "Id", "Name");
            return View();
        }

        // POST: IncompatibleAgricultures/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,AgricultureTypeId,AgricultureName")] IncompatibleAgriculture incompatibleAgriculture)
        {
            if (ModelState.IsValid)
            {
                db.IncompatibleAgricultures.Add(incompatibleAgriculture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgricultureTypeId = new SelectList(db.AgricultureTypes, "Id", "Name", incompatibleAgriculture.AgricultureTypeId);
            return View(incompatibleAgriculture);
        }

        // GET: IncompatibleAgricultures/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompatibleAgriculture incompatibleAgriculture = db.IncompatibleAgricultures.Find(id);
            if (incompatibleAgriculture == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgricultureTypeId = new SelectList(db.AgricultureTypes, "Id", "Name", incompatibleAgriculture.AgricultureTypeId);
            return View(incompatibleAgriculture);
        }

        // POST: IncompatibleAgricultures/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,AgricultureTypeId,AgricultureName")] IncompatibleAgriculture incompatibleAgriculture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incompatibleAgriculture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgricultureTypeId = new SelectList(db.AgricultureTypes, "Id", "Name", incompatibleAgriculture.AgricultureTypeId);
            return View(incompatibleAgriculture);
        }

        // GET: IncompatibleAgricultures/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncompatibleAgriculture incompatibleAgriculture = db.IncompatibleAgricultures.Find(id);
            if (incompatibleAgriculture == null)
            {
                return HttpNotFound();
            }
            return View(incompatibleAgriculture);
        }

        // POST: IncompatibleAgricultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            IncompatibleAgriculture incompatibleAgriculture = db.IncompatibleAgricultures.Find(id);
            db.IncompatibleAgricultures.Remove(incompatibleAgriculture);
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
