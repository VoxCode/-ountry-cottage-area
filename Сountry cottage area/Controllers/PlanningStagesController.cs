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
    public class PlanningStagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlanningStages
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.PlanningStages.ToList());
        }

        // GET: PlanningStages/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanningStage planningStage = db.PlanningStages.Find(id);
            if (planningStage == null)
            {
                return HttpNotFound();
            }
            return View(planningStage);
        }

        // GET: PlanningStages/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanningStages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "Id,Content,Date,AreaId")] PlanningStage planningStage)
        {
            if (ModelState.IsValid)
            {
                db.PlanningStages.Add(planningStage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planningStage);
        }

        // GET: PlanningStages/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanningStage planningStage = db.PlanningStages.Find(id);
            if (planningStage == null)
            {
                return HttpNotFound();
            }
            return View(planningStage);
        }

        // POST: PlanningStages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,Content,Date,AreaId")] PlanningStage planningStage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planningStage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planningStage);
        }

        // GET: PlanningStages/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanningStage planningStage = db.PlanningStages.Find(id);
            if (planningStage == null)
            {
                return HttpNotFound();
            }
            return View(planningStage);
        }

        // POST: PlanningStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanningStage planningStage = db.PlanningStages.Find(id);
            db.PlanningStages.Remove(planningStage);
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
