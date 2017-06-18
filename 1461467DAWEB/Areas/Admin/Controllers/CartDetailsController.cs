using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1461467DAWEB.Areas.Admin.Controllers
{

    [Authorize(Roles ="Manager, Admin")]
    public class CartDetailsController : Controller
    {
        // GET: Admin/CartDetails
        public ActionResult Index(int id,  int Page=1)
        {
            return View(Models.View_CTDH.DanhSach(id, Page, 5));
        }
    }
}