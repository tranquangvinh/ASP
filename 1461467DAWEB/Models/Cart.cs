using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class Cart
    {
        public static void InserCart(int mtk, int sl, int tt)
        {
            var db = new ShopConnectionDB();
            GioHang gh = new GioHang();
            gh.MaTaiKhoan = mtk;
            gh.SoLuong = sl;
            gh.TongTien = tt;
            db.Insert(gh);
        }
    }
}