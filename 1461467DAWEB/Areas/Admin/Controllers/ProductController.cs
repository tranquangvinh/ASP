using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(int page=1)
        {
            var resultProduct = Models.ProductType_Manufacturer.ListProduct(page, 5);
            return View(resultProduct);
        }
        public ActionResult Search(String Key)
        {
            return View(Models.ProductType_Manufacturer.SearchAccount(Key));
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
            Session["ID"] = sp.MaSanPham;
            return RedirectToAction("listImg");
            
        }

        public ActionResult listImg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addListImg(String IdSP)
        {
            string pathValue = Server.MapPath("~/");
            var hpt = HttpContext.Request.Files[0];
            String link = "";
            int id = -1;
            for (int i = 0; i < 4; i++)
            {
                hpt = HttpContext.Request.Files[i];
                if (hpt.FileName == "")
                {

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
                            link = fullNameImage;
                            id = Int32.Parse(IdSP);

                        }
                    }
                    Models.Product.insertImg(link, id);
                }
            }
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
            Session["ID"] = sp.MaSanPham;
            return RedirectToAction("EditImg");
        }

        public ActionResult EditImg()
        {
            int id = Int32.Parse(Session["ID"].ToString());
            Session.Remove("ID");
            var listImg = Models.Product.GetListImg(id);
            return View(listImg);
        }
        [HttpPost]
        public ActionResult updateListImg(int []IdHinhSP)
        {
            string pathValue = Server.MapPath("~/");
            var hpt = HttpContext.Request.Files[0];
            String link = "";
            for (int i = 0; i < 4; i++)
            {
                hpt = HttpContext.Request.Files[i];
                if (hpt.FileName == "")
                {

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
                            link = fullNameImage;

                        }
                    }

                     
                    if(IdHinhSP[i] != -1)
                    {
                        Models.Product.updateListImage(IdHinhSP[i], link);
                    }
                }
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
