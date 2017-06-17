using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class CartDetails
    {
        public static void InsertCartDetails(int idgh, int idsp, int sl, Decimal tt) { 
            var db = new ShopConnectionDB();
            ChiTietDonHang ctdh = new ChiTietDonHang();
            ctdh.idGioHang = idgh;
            ctdh.idMaSanPham = idsp;
            ctdh.SoLuong = sl;
            ctdh.TongTien = tt;
            db.Insert(ctdh);
        }
    }
}