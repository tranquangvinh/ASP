using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Controllers
{
    public class ManufacturerController : Controller
    {
        // GET: Admin/Manufacturer
        public ActionResult Index(int Page = 1)
        {
            var resultManufacturer = Models.Manufacturer.ByListManufacturer(Page , 1);
            return View(resultManufacturer);
        }


        // GET: Admin/Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manufacturer/Create
        [HttpPost]
        public ActionResult Create(HangSanXuat hsx)
        {
            try
            {
                // TODO: Add insert logic here
                _1461467DAWEB.Areas.Admin.Models.Manufacturer.insertManufacturer(hsx);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Manufacturer/Edit/5
        public ActionResult Edit(int id)
        {
            var resutlDetailsProductType = Models.Manufacturer.GetManufacturer(id);
            return View(resutlDetailsProductType);
        }

        // POST: Admin/Manufacturer/Edit/5
        [HttpPost]
        public ActionResult Edit(HangSanXuat hsx)
        {
            Models.Manufacturer.UpdateManufacturer(hsx);
            return RedirectToAction("Index");
        }

         
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Models.Manufacturer.DeleteManufacturer(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
