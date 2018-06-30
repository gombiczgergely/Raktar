using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Raktar.DAL;
using Raktar.Models;

namespace Raktar.Controllers
{
    public class StockController : Controller
    {
        private RaktarContext2 db = new RaktarContext2();

        // GET: Stock
        public ActionResult Index()
        {
            var stock = db.Stock.Include(s => s.Equipment);
            return View(stock.ToList());
        }

        // GET: Stock/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            return View(stock);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentID = new SelectList(db.Equipment, "EquipmentID", "Describe");
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockID,EquipmentID,InStock,SQuantity")] Stock stock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Stock.Add(stock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            EquipmentDropDownList(stock.EquipmentID);
            
            return View(stock);
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            if (stock == null)
            {
                return HttpNotFound();
            }
            EquipmentDropDownList(stock.EquipmentID);
            
            return View(stock);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stockToUpdate = db.Stock.Find(id);
            if (TryUpdateModel(stockToUpdate, "",
               new string[] { "StockID","EquipmentID","InStock","SQuantity" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            EquipmentDropDownList(stockToUpdate.EquipmentID);
            
            return View(stockToUpdate);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stock stock = db.Stock.Find(id);
            db.Stock.Remove(stock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void EquipmentDropDownList(object selectedEquipment = null)
        {
            var equipmentQuery = from d in db.Equipment
                                 orderby d.Brand
                                 select d;
            ViewBag.EquipmentId = new SelectList(equipmentQuery, "EquipmentId", "Type", "Brand", selectedEquipment);
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
