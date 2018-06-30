using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Raktar.DAL;
using Raktar.Models;
using PagedList;

namespace Raktar.Controllers
{
    public class RentersController : Controller
    {
        private RaktarContext2 db = new RaktarContext2();

        // GET: Renters
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "city";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var renter = from s in db.Renter
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                renter = renter.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    renter = renter.OrderByDescending(s => s.Name);
                    break;
                case "city_desc":
                    renter = renter.OrderByDescending(s => s.City);
                    break;
                case "city":
                    renter = renter.OrderBy(s => s.City);
                    break;

                default:  // Name ascending 
                    renter = renter.OrderBy(s => s.Name);
                    break;
            }


            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(renter.ToPagedList(pageNumber, pageSize));
        }

        // GET: Renters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renter.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // GET: Renters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Renters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RenterID,Name,City,ZipCode,StreetAddress,Phone,Mail")] Renter renter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Renter.Add(renter);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            catch(DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(renter);
        }

        // GET: Renters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renter.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // POST: Renters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RenterID,Name,City,ZipCode,StreetAddress,Phone,Mail")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(renter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(renter);
        }

        // GET: Renters/Delete/5
        public ActionResult Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Renter renter = db.Renter.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                Renter renter = db.Renter.Find(id);
                db.Renter.Remove(renter);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
