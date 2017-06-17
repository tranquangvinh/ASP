using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class Manufacturer
    {
        public static Page<HangSanXuat> ByListManufacturer(int pageNumber, int itemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<HangSanXuat>(pageNumber, itemPage, " select * from HangSanXuat");
        }
        public static IEnumerable<HangSanXuat> ListManufacturer()
        {
            var db = new ShopConnectionDB();
            return db.Query<HangSanXuat>("select * from HangSanXuat");
        }
        public static void insertManufacturer(HangSanXuat lsp)
        {
            lsp.Insert();
        }

        public static IEnumerable<HangSanXuat> SearchAccount(String key)
        {
            var db = new ShopConnectionDB();
            return db.Fetch<HangSanXuat>("select * from HangSanXuat where TenHangSanXuat LIKE @0", "%" + key + "%");
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