using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Raktar.DAL;
using Raktar.Models;

namespace Raktar.Controllers
{
    public class EquipmentController : Controller
    {
        private RaktarContext2 db = new RaktarContext2();

        // GET: Equipment
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.PageSize = new List<SelectListItem>()
         {
             new SelectListItem() { Value="5", Text= "5" },
             new SelectListItem() { Value="10", Text= "10" },
             new SelectListItem() { Value="15", Text= "15" },
             new SelectListItem() { Value="25", Text= "25" },
             new SelectListItem() { Value="50", Text= "50" },
         };
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DescriptSortParm = String.IsNullOrEmpty(sortOrder) ? "decript_desc" : "";
            ViewBag.BrandSortParm = String.IsNullOrEmpty(sortOrder) ? "brand_desc" : "brand";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var equipment = from s in db.Equipment
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                equipment = equipment.Where(s => s.Brand.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "decript_desc":
                    equipment = equipment.OrderByDescending(s => s.Describe);
                    break;
                case "brand_desc":
                    equipment = equipment.OrderByDescending(s => s.Brand);
                    break;
                case "brand":
                    equipment = equipment.OrderBy(s => s.Brand);
                    break;

                default:  // Name ascending 
                    equipment = equipment.OrderBy(s => s.Describe);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(equipment.ToPagedList(pageNumber, pageSize));

        }

        // GET: Equipment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentID,Describe,Brand,Type,AdditionalBrand,AdditionalType,AdditionalDescribe,NettoPrice,BruttoPrice")] Equipment equipment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Equipment.Add(equipment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(equipment);
        }

        // GET: Equipment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentID,Describe,Brand,Type,AdditionalBrand,AdditionalType,AdditionalDescribe,NettoPrice,BruttoPrice")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipment);
        }

        // GET: Equipment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipment.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Equipment equipment = db.Equipment.Find(id);
            db.Equipment.Remove(equipment);
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
