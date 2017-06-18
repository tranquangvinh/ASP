using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Controllers
{
    [Authorize]
    public class BinhLuanController : Controller
    {
        // GET: BinhLuan
        public ActionResult Create(int MaSanPham, String NoiDung)
        {
            Models.BinhLuan.insert(NoiDung, MaSanPham, User.Identity.Name);

            return RedirectToAction("Details", "Products", new { id = MaSanPham });
        }
    }
}