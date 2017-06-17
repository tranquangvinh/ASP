using _1461467DAWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if(Session["Cart"] != null) {
                List<Item> cart = (List<Item>)Session["Cart"];
                ViewBag.TongTien = 0;
                for (int i = 0; i < cart.Count; i++)
                {
                    ViewBag.TongTien = ViewBag.TongTien + (cart[i].GetSL() * cart[i].GetSP().Gia);
                }
            }
            return View();
        }
        public int CheckIDSP(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            for(int i = 0; i < cart.Count; i++)
            {
                if(cart[i].GetSP().MaSanPham == id)
                {
                    return i;
                }
            }
            return -1;
        }
        [HttpPost]
        public ActionResult Update(int []Quantity)
        {
              
            List<Item> cart = (List<Item>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].setQuantity(Quantity[i]);
                if (Quantity[i] < 1 || Quantity[i] == null || Quantity[i] > 100)
                {
                    cart[i].setQuantity(1);
                }
               
            }
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult Create(int id)
        {
            int sl = 1;
            var F = HttpContext.Request.Form;
            if (HttpContext.Request.Form.Count > 0) {
                sl = Int32.Parse(HttpContext.Request.Form["sl"]);
            }
            
            if (sl < 1)
            {
                sl = 1;
            }

            if (Session["Cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(SanPhamBus.ListProductsDetails(id), sl));
                Session["Cart"] = cart;
            }
            else
            {
                if(CheckIDSP(id) == -1)
                {
                    List<Item> cart = (List<Item>)Session["Cart"];
                    cart.Add(new Item(SanPhamBus.ListProductsDetails(id), sl));
                    Session["Cart"] = cart;
                }
                else
                {
                    List<Item> cart = (List<Item>)Session["Cart"];
                    cart[CheckIDSP(id)].setQuantity(cart[CheckIDSP(id)].GetSL() + sl);
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            List<Item> cart = (List<Item>)Session["Cart"];
            int index = CheckIDSP(id);
            cart.RemoveAt(index);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ThanhToan()

        {
            if (Session["Cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["Cart"];
                ViewBag.TongTien = 0;
                for (int i = 0; i < cart.Count; i++)
                {
                    ViewBag.TongTien = ViewBag.TongTien + (cart[i].GetSL() * cart[i].GetSP().Gia);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult ThanhToanTien()
        {
            var F = HttpContext.Request.Form;
            if (HttpContext.Request.Form.Count > 0)
            {
                String ten  =  HttpContext.Request.Form["Ten"].ToString();
                String Email = HttpContext.Request.Form["Email"].ToString();
                String Phone = HttpContext.Request.Form["Phone"].ToString();
                String DiaChi = HttpContext.Request.Form["DiaChi"].ToString();
               
               // Cart.InserCart();
            }
            return View();
        }
    }
}