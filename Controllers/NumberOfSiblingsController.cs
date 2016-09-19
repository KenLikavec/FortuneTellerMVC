using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fortune_Teller_MVC.Models;

namespace Fortune_Teller_MVC.Controllers
{
    public class NumberOfSiblingsController : Controller
    {
        private FortuneTellerDBEntities1 db = new FortuneTellerDBEntities1();

        // GET: NumberOfSiblings
        public ActionResult Index()
        {
            return View(db.NumberOfSiblings.ToList());
        }

        // GET: NumberOfSiblings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberOfSibling numberOfSibling = db.NumberOfSiblings.Find(id);
            if (numberOfSibling == null)
            {
                return HttpNotFound();
            }
            return View(numberOfSibling);
        }

        // GET: NumberOfSiblings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NumberOfSiblings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumberOfSiblingsID,NumOfSibs")] NumberOfSibling numberOfSibling)
        {
            if (ModelState.IsValid)
            {
                db.NumberOfSiblings.Add(numberOfSibling);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(numberOfSibling);
        }

        // GET: NumberOfSiblings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberOfSibling numberOfSibling = db.NumberOfSiblings.Find(id);
            if (numberOfSibling == null)
            {
                return HttpNotFound();
            }
            return View(numberOfSibling);
        }

        // POST: NumberOfSiblings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NumberOfSiblingsID,NumOfSibs")] NumberOfSibling numberOfSibling)
        {
            if (ModelState.IsValid)
            {
                db.Entry(numberOfSibling).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(numberOfSibling);
        }

        // GET: NumberOfSiblings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberOfSibling numberOfSibling = db.NumberOfSiblings.Find(id);
            if (numberOfSibling == null)
            {
                return HttpNotFound();
            }
            return View(numberOfSibling);
        }

        // POST: NumberOfSiblings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NumberOfSibling numberOfSibling = db.NumberOfSiblings.Find(id);
            db.NumberOfSiblings.Remove(numberOfSibling);
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
