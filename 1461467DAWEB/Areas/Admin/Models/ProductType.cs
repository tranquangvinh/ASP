using ShopConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class ProductType
    {
        public static Page<LoaiSanPham> ByListProductType(int pageNumber,int itemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<LoaiSanPham>(pageNumber, itemPage,"select * from LoaiSanPham");
        }

        public static IEnumerable<LoaiSanPham> ListProductType()
        {
            var db = new ShopConnectionDB();
            return db.Query<LoaiSanPham>("select * from LoaiSanPham");
        }

        public static void insertProductType(LoaiSanPham lsp)
        {
            lsp.Insert();
        }
        public static IEnumerable<LoaiSanPham> SearchListProductType(string key)
        {
            using (var db = new ShopConnectionDB())
            {
                var result = db.Fetch<LoaiSanPham>("select * from LoaiSanPham where TenLoaiSanPham LIKE @0","%" + key + "%");
                return result;
            }

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