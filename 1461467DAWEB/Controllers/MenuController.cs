using _1461467DAWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace _1461467DAWEB.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult MenuLoaiSanPham()
        {
            return View(LoaiSanPhamBus.DanhSach());
        }

        public ActionResult MenuHangSanXuat()
        {

            return View(HangSanXuatBus.DanhSach());
        }

        public ActionResult SearchSP()
        {
            return View();
        }

    }
}