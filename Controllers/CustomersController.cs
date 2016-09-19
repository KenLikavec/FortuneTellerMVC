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
    public class CustomersController : Controller
    {
        private FortuneTellerDBEntities1 db = new FortuneTellerDBEntities1();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.FavoriteColor).Include(c => c.NumberOfSibling);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }


            //Generated input into details view for amount of years until retirement portion of fortune
            if (customer.Age % 2 == 0)
            {
                // even
                ViewBag.NumberOfYears = 1;
            }
            else
            {
                ViewBag.NumberOfYears = 401;
            }

            //Generated input into details view for amount of money portion of fortune
            var firstLetterOfMonth = customer.BirthMonth.BirthMonthName[0];
            var secondLetterOfMonth = customer.BirthMonth.BirthMonthName[1];
            var thirdLetterOfMonth = customer.BirthMonth.BirthMonthName[2];
            string wholeName = customer.FirstName + customer.LastName;
            if (wholeName.Contains(firstLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 1000000;
            }
            else if (wholeName.Contains(secondLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 3700;
            }
            else if (wholeName.Contains(thirdLetterOfMonth))
            {
                ViewBag.AmountOfMoney = 250000;
            }
            else
            {
                ViewBag.AmountOfMoney = 2;
            }




            //Generated input into details view for location portion of fortune
            if (customer.NumberOfSibling.NumberOfSiblingsID == 0)
            {
                ViewBag.Location = "The Shire";
            }
            else if (customer.NumberOfSibling.NumberOfSiblingsID == 1)
            {
                ViewBag.Location = "Antartica";
            }
            else if (customer.NumberOfSibling.NumberOfSiblingsID == 2)
            {
                ViewBag.Location = "Bermuda Triangle";
            }
            else if (customer.NumberOfSibling.NumberOfSiblingsID == 3)
            {
                ViewBag.Location = "Dagoba";
            }
            else
            {
                ViewBag.Location = "Jurassic Park";
            }



            //Generated input into details view for transportaion portion of fortune
            if (customer.FavoriteColor.FavoriteColorID == 0)
            {
                ViewBag.ModeOfTrans = "jet pack";
            }
            else if (customer.FavoriteColor.FavoriteColorID == 1)
            {
                ViewBag.ModeOfTrans = "space ship";
            }
            else if (customer.FavoriteColor.FavoriteColorID == 2)
            {
                ViewBag.ModeOfTrans = "ferarri";
            }
            else if (customer.FavoriteColor.FavoriteColorID == 3)
            {
                ViewBag.ModeOfTrans = "canoe";
            }
            else if (customer.FavoriteColor.FavoriteColorID == 4)
            {
                ViewBag.ModeOfTrans = "hover craft";
            }
            else if (customer.FavoriteColor.FavoriteColorID == 5)
            {
                ViewBag.ModeOfTrans = "sail boat";
            }
            else
            {
                ViewBag.ModeOfTrans = "uni-cycle";
            }





            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName");
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColorName");
            ViewBag.NumberOfSiblingsID = new SelectList(db.NumberOfSiblings, "NumberOfSiblingsID", "NumOfSibs");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //redirects customer straight to details page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BirthMonthID,FavoriteColorID,NumberOfSiblingsID,FirstName,LastName,Age,CustomerID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = customer.CustomerID });
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColorName", customer.FavoriteColorID);
            ViewBag.NumberOfSiblingsID = new SelectList(db.NumberOfSiblings, "NumberOfSiblingsID", "NumOfSibs", customer.NumberOfSiblingsID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColorName", customer.FavoriteColorID);
            ViewBag.NumberOfSiblingsID = new SelectList(db.NumberOfSiblings, "NumberOfSiblingsID", "NumOfSibs", customer.NumberOfSiblingsID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BirthMonthID,FavoriteColorID,NumberOfSiblingsID,FirstName,LastName,Age,CustomerID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColorName", customer.FavoriteColorID);
            ViewBag.NumberOfSiblingsID = new SelectList(db.NumberOfSiblings, "NumberOfSiblingsID", "NumOfSibs", customer.NumberOfSiblingsID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
