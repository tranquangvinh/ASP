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
        public ActionResult Index(int page=1)
        {
            var resultProduct = Models.ProductType_Manufacturer.ListProduct(page, 2);
            return View(resultProduct);
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
            ViewBag.Manufacturer = Models.Manufacturer.ListManufacturer();
            ViewBag.ProductType = Models.ProductType.ListProductType();
            var ProductDetails = Models.Product.GetProduct(id);
            return View(ProductDetails);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SanPham sp)
        {
            string pathValue = Server.MapPath("~/");

            var hpt = HttpContext.Request.Files[0];
            if (hpt.FileName == "")
            {
                Models.Product.UpdateProduct(sp, 0);
            }
            else
            {
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
                Models.Product.UpdateProduct(sp, 1);
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                Models.Product.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
