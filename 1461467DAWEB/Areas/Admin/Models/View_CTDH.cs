using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class View_CTDH
    {
        public static Page<View_ChiTieDonHang> DanhSach(int id, int Page , int ItemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<View_ChiTieDonHang>(Page, ItemPage, "select * from View_ChiTieDonHang where idGioHang=@0", id);
        }
    }
}