using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class Manufacturer
    {
        public static IEnumerable<HangSanXuat> ListManufacturer()
        {
            var db = new ShopConnectionDB();
            return db.Query<HangSanXuat>("select * from HangSanXuat");
        }

        public static void insertManufacturer(HangSanXuat lsp)
        {
            lsp.Insert();
        }

        public static HangSanXuat GetManufacturer(int id)
        {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<HangSanXuat>("select * from HangSanXuat where MaHangSanXuat=@0", id);
        }

        public static void UpdateManufacturer(HangSanXuat hsx)
        {
            using (var db = new ShopConnectionDB())
            {
                db.Update<HangSanXuat>("SET TenHangSanXuat=@0 WHERE MaHangSanXuat=@1", hsx.TenHangSanXuat, hsx.MaHangSanXuat);
            }
        }

        public static void DeleteManufacturer(int id)
        {
            try
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Execute("delete from HangSanXuat where MaHangSanXuat = @0", id);
                }
            }
            catch
            {
            }
        }
    }
}