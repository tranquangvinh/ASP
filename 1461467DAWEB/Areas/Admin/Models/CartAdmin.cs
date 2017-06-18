using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class CartAdmin
    {
        public static GioHang Details(int id)
        {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<GioHang>("select * from GioHang where MaGioHang=@0", id);
        }
        public static void UpdateCart(GioHang gh)
        {
            using (var db = new ShopConnectionDB())
            {
                db.Update<GioHang>("SET TenNguoiMua=@0, DiaChi=@1 , Email=@2 , Phone=@3 , TrangThai=@4 WHERE MaGioHang=@5", gh.TenNguoiMua, gh.DiaChi, gh.Email, gh.Phone, gh.TrangThai, gh.MaGioHang);
            }
        }
        public static void DeleteCart(int id)
        {
            try
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Execute("delete from GioHang where MaGioHang= @0", id);
                    db.Execute("delete from ChiTietDonHang where idGioHang= @0", id);
                }
            }
            catch
            {

            }
        }
    }
}