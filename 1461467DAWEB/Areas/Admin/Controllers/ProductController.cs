using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin") ]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            var resultProduct = Models.ProductType_Manufacturer.ListProduct();
            return View(resultProduct);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.Manufacturer = Models.Manufacturer.ListManufacturer();
            ViewBag.ProductType = Models.ProductType.ListProductType();
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SanPham sp)
        {
            string pathValue = Server.MapPath("~/");

            var hpt = HttpContext.Request.Files[0];
            if (HttpContext.Request.Files.Count > 0)
            {
                if (hpt.ContentLength > 0)
                {
                    string temp = hpt.FileName;
                    string RDString = Guid.NewGuid().ToString();
                    string fullNameImage = "upload/img/" + RDString + temp;
                    hpt.SaveAs(pathValue + fullNameImage);
                    sp.TenHinh = fullNameImage;
                }
            }

            Models.Product.insertProduct(sp);
            return RedirectToAction("Index");
            
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Models.Product.GetProduct(id));
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
