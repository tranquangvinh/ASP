using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class View_GH
    {
        public static Page<View_GioHang> DanhSach(int Page, int itemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<View_GioHang>(Page, itemPage,  "select * from View_GioHang");
        }
    }
}