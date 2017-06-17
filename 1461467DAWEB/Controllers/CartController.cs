using _1461467DAWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
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
        public ActionResult Create(int id)
        {
            if (Session["Cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(SanPhamBus.ListProductsDetails(id), 1));
                Session["Cart"] = cart;
            }
            else
            {
                if(CheckIDSP(id) == -1)
                {
                    List<Item> cart = (List<Item>)Session["Cart"];
                    cart.Add(new Item(SanPhamBus.ListProductsDetails(id), 1));
                    Session["Cart"] = cart;
                }
                else
                {
                    List<Item> cart = (List<Item>)Session["Cart"];
                    cart[CheckIDSP(id)].setQuantity(cart[CheckIDSP(id)].GetSL() + 1);
                    Session["Cart"] = cart;
                }
            }
            return RedirectToAction("Index");
        }
    }
}