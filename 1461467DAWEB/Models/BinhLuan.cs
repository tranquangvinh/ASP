using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class BinhLuan
    {
        public static void insert(string noidung, int MSP, String UserName)
        {
            using (var db = new ShopConnectionDB())
            {
                BinhLuanSanPham bl = new BinhLuanSanPham();
                bl.MaSanPham = MSP;
                bl.NoiDung = noidung;
                bl.EmailBinhLuan = UserName;
                db.Insert(bl);
            }
        }
        public static IEnumerable<BinhLuanSanPham> ListComments(int id)
        {
            var db = new ShopConnectionDB();
            String query = "select * from BinhLuanSanPham where MaSanPham = " + id;
            return db.Query<BinhLuanSanPham>(query);
        }
    }
}