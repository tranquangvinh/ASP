using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Controllers
{
    [Authorize(Roles ="Manager, Admin")]
    public class CartController : Controller
    {
        // GET: Admin/Cart
        public ActionResult Index(int Page=1)
        {
            return View(Models.View_GH.DanhSach(Page, 3));
        }
        public ActionResult Edit(int id)
        {
            return View(Models.Cart.Details(id));
        }

        [HttpPost]
        public ActionResult Edit(GioHang gh)
        {
            Models.Cart.UpdateCart(gh);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Models.Cart.DeleteCart(id);
            return RedirectToAction("Index");
        }

    }
}