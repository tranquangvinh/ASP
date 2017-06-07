using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            var resultListAcount = Models.AspNetUsersModel.ListAccount();
            return View(resultListAcount);
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Account/Edit/5
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

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Account/Delete/5
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
