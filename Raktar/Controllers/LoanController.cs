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
    public class LoanController : Controller
    {
        private RaktarContext2 db = new RaktarContext2();

        // GET: Loan
        public ActionResult Index()
        {
            var loan = db.Loan.Include(l => l.Equipment).Include(l => l.Renter);
            return View(loan.ToList());
        }

        // GET: Loan/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loan/Create
        public ActionResult Create()
        {
            EquipmentDropDownList();
            RenterDropDownList();

            return View();
        }

        // POST: Loan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanID,RenterID,EquipmentID,Quantity,Date")] Loan loan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Loan.Add(loan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            EquipmentDropDownList(loan.EquipmentID);
            RenterDropDownList(loan.RenterID);
            return View(loan);
        }

        // GET: Loan/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            EquipmentDropDownList(loan.EquipmentID);
            RenterDropDownList(loan.RenterID);
            return View(loan);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        
        public ActionResult EditPost(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var loanToUpdate = db.Loan.Find(id);
            if (TryUpdateModel(loanToUpdate, "",
               new string[] { "LoanID","RenterID","EquipmentID","Quantity,Date" }))
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
            EquipmentDropDownList(loanToUpdate.EquipmentID);
            RenterDropDownList(loanToUpdate.RenterID);
            return View(loanToUpdate);
        }

        private void EquipmentDropDownList(object selectedEquipment = null)
        {
            var equipmentQuery = from d in db.Equipment
                                   orderby d.Brand
                                   select d;
            ViewBag.EquipmentId = new SelectList(equipmentQuery, "EquipmentId", "Type", "Brand", selectedEquipment);
        }

        private void LoanDropDownList(object selectedLoan = null)
        {
            var LoanQuery = from d in db.Loan
                              orderby d.LoanID
                              select d;
            ViewBag.LoanID = new SelectList(LoanQuery, "LoanID", "LoanID", selectedLoan);
        }

        private void RenterDropDownList(object selectedRenter = null)
        {
            var renterQuery = from d in db.Renter
                                 orderby d.Name
                                 select d;
            ViewBag.RenterID = new SelectList(renterQuery, "RenterID", "Name", selectedRenter);
        }


        // GET: Loan/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loan.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Loan loan = db.Loan.Find(id);
            db.Loan.Remove(loan);
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
