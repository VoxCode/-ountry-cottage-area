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
    public class AgricultureTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgricultureTypes
        public ActionResult Index()
        {
            var agricultureTypes = db.AgricultureTypes.Include(a => a.AgriculturesCategory);
            return View(agricultureTypes.ToList());
        }

        // GET: AgricultureTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureType agricultureType = db.AgricultureTypes.Find(id);
            if (agricultureType == null)
            {
                return HttpNotFound();
            }
            return View(agricultureType);
        }

        // GET: AgricultureTypes/Create
        public ActionResult Create()
        {
            ViewBag.AgriculturesCategoryId = new SelectList(db.AgriculturesCategories, "Id", "Name");
            return View();
        }

        // POST: AgricultureTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AgriculturesCategoryId,Index,Content")] AgricultureType agricultureType)
        {
            if (ModelState.IsValid)
            {
                db.AgricultureTypes.Add(agricultureType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgriculturesCategoryId = new SelectList(db.AgriculturesCategories, "Id", "Name", agricultureType.AgriculturesCategoryId);
            return View(agricultureType);
        }

        // GET: AgricultureTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureType agricultureType = db.AgricultureTypes.Find(id);
            if (agricultureType == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgriculturesCategoryId = new SelectList(db.AgriculturesCategories, "Id", "Name", agricultureType.AgriculturesCategoryId);
            return View(agricultureType);
        }

        // POST: AgricultureTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AgriculturesCategoryId,Index,Content")] AgricultureType agricultureType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agricultureType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgriculturesCategoryId = new SelectList(db.AgriculturesCategories, "Id", "Name", agricultureType.AgriculturesCategoryId);
            return View(agricultureType);
        }

        // GET: AgricultureTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureType agricultureType = db.AgricultureTypes.Find(id);
            if (agricultureType == null)
            {
                return HttpNotFound();
            }
            return View(agricultureType);
        }

        // POST: AgricultureTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgricultureType agricultureType = db.AgricultureTypes.Find(id);
            db.AgricultureTypes.Remove(agricultureType);
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
