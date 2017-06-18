using _1461467DAWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index(int page = 1)
        {
            var dssp = SanPhamBus.ListProduct(page, 8);
            return View(dssp);
        }
        public ActionResult Details(int id)
        {
            var ctdssp = SanPhamBus.ListProductsDetails(id);
            ViewBag.ListImg = ListImg.DanhSachHinhSanPham(id);
            ViewBag.SanPhamLienQuan = SanPhamBus.SanPhamLienQuan(id, 4, ctdssp.LoaiSp);
            ViewBag.ListComments = BinhLuan.ListComments(id);
            return View(ctdssp);
        }
        public ActionResult Type(int type)
        {
            var ctdssp = SanPhamBus.ListProductsType(type);
            return View(ctdssp);
        }
        public ActionResult Made(int made)
        {
            var ctdssp = SanPhamBus.ListProducer(made);
            return View(ctdssp);
        }
        public ActionResult SanPhamHSX(int id, int page=1)
        {
            var dssp = SanPhamBus.ListProductPageHSX(id, page, 8);
            return View(dssp);
        }
        public ActionResult SanPhamLSP(int id, int page = 1)
        {
            var dssp = SanPhamBus.ListProductPage(id, page, 8);
            return View(dssp);
        }
    }
}