using ShopConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1461467DAWEB.Areas.Admin.Models
{
    public class ProductType
    {
        public static IEnumerable<LoaiSanPham> ListProductType()
        {
            var db = new ShopConnectionDB();
            return db.Query<LoaiSanPham>("select * from LoaiSanPham");
        }

        public static void insertProductType(LoaiSanPham lsp)
        {
            lsp.Insert();
        }

        public static LoaiSanPham GetProductType(int id)
        {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<LoaiSanPham>("select * from LoaiSanPham where MaLoaiSanPham=@0", id);
        }

        public static void UpdateProductType(LoaiSanPham lsp)
        {
            using (var db = new ShopConnectionDB())
            {
                db.Update<LoaiSanPham>("SET TenLoaiSanPham=@0 WHERE MaLoaiSanPham=@1", lsp.TenLoaiSanPham, lsp.MaLoaiSanPham);
            }
        }

        public static void DeleteProductType(int id)
        {
            try
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Execute("delete from loaisanpham where MaLoaiSanPham = @0", id);
                }
            }
            catch
            {
            }
        }
    }
}