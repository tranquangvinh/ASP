using PetaPoco;
using ShopConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1461467DAWEB.Models
{
    public class SanPhamBus
    {
        public static Page<SanPham> ListProduct(int pageNumber, int itemPrePage)
        {
            var db = new ShopConnectionDB();
            return db.Page<SanPham>(pageNumber, itemPrePage, "select * from SanPham");
        }
        public static Page<SanPham> ListProductPage(int id, int pageNumber, int itemPrePage)
        {
            var db = new ShopConnectionDB();
            return db.Page<SanPham>(pageNumber, itemPrePage, "select * from SanPham where LoaiSp=@0", id);
        }
        public static Page<SanPham> ListProductPageHSX(int id, int pageNumber, int itemPrePage)
        {
            var db = new ShopConnectionDB();
            return db.Page<SanPham>(pageNumber, itemPrePage, "select * from SanPham where HangSx=@0", id);
        }
        public static IEnumerable<SanPham> ListProductNew()
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select top 8 * from SanPham ORDER BY MaSanPham DESC");
        }
        public static IEnumerable<SanPham> ListProductHighlights()
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select top 8 * from SanPham where NoiBat=1 ");
        }
        public static IEnumerable<SanPham> ListProductsSell()
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select top 8 * from SanPham ORDER BY SoLuongBan DESC  ");
        }
        public static IEnumerable<SanPham> ListProductsViews()
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select top 8 * from SanPham ORDER BY SoLuongXem DESC  ");
        }
        public static SanPham ListProductsDetails(int id)
        {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<SanPham>("select * from SanPham where MaSanPham = @0", id);
        }
        public static IEnumerable<SanPham> ListProductsType(int type)
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select * from SanPham where LoaiSP = @0", type);
        }
        public static IEnumerable<SanPham> ListProducer(int made)
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select * from SanPham where HangXS = @0", made);
        }

        public static IEnumerable<SanPham> SanPhamLienQuan(int idSP, int limit, int? LSP)
        {
            var db = new ShopConnectionDB();
            String query = "select top " + limit + " * from sanpham where MaSanPham not in (" + idSP + ") and LoaiSp = " + LSP;
            return db.Query<SanPham>(query);
        }

        public static IEnumerable<SanPham> SearchProduct(String key)
        {
            var db = new ShopConnectionDB();
            return db.Fetch<SanPham>("select * from SanPham where TenSanPham LIKE @0", "%" + key + "%");
        }
    }
}