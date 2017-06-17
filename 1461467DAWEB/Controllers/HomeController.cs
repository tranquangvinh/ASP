
using _1461467DAWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var spmoi = SanPhamBus.ListProductNew();
            ViewBag.spnb = SanPhamBus.ListProductHighlights();
            ViewBag.spbc = SanPhamBus.ListProductsSell();
            ViewBag.spxn = SanPhamBus.ListProductsViews();
            return View(spmoi);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}