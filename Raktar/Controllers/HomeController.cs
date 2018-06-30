using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raktar.DAL;
using Raktar.ViewModels;

namespace Raktar.Controllers
{
    public class HomeController : Controller
    {
        private RaktarContext2 db = new RaktarContext2();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            /*IQueryable<RenterCityGroup> data = from renter in db.Renter
                                                   group renter by renter.City into dateGroup
                                                   select new RenterCityGroup()
                                                   {
                                                       RenterCity = dateGroup.Key,
                                                       RenterCount = dateGroup.Count()
                                                   };*/
           // return View(data.ToList());

            
        ViewBag.Message = "Ez egy raktár applikáció.";

        return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}