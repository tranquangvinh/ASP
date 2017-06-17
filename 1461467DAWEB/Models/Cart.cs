using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class Cart
    {
        public static GioHang InserCart(String mtk, String Ten, String DiaChi, String Email, String Phone, Decimal tt)
        {
            var db = new ShopConnectionDB();
            GioHang gh = new GioHang();
            gh.MaTaiKhoan = mtk;
            gh.TenNguoiMua = Ten;
            gh.DiaChi = DiaChi;
            gh.Email = Email;
            gh.Phone = Phone;
            gh.TongTien = tt;
            db.Insert(gh);
            return gh;
        }
    }
}