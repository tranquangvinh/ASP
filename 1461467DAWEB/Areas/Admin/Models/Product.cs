using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class Product
    {
        public static IEnumerable<SanPham> ListProduct()
        {
            var db = new ShopConnectionDB();
            return db.Query<SanPham>("select * from SanPham");
        }

        public static void insertProduct(SanPham sp)
        {
            sp.Insert();
        }

        public static SanPham GetProduct(int id)
        {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<SanPham>("select * from SanPham where MaSanPham=@0", id);
        }

        public static void UpdateProduct(SanPham sp, int valueImg)
        {
            if (valueImg == 0)
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Update<SanPham>("SET TenSanPham=@0, Gia=@1 , GiamGia=@2 , MoTa=@3 , LoaiSp=@4 , HangSx=@5  WHERE MaSanPham=@6", sp.TenSanPham, sp.Gia, sp.GiamGia, sp.MoTa, sp.LoaiSp, sp.HangSx, sp.MaSanPham);
                }
            }
            else
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Update<SanPham>("SET TenSanPham=@0, Gia=@1 , GiamGia=@2 , TenHinh=@3 , MoTa=@4 , LoaiSp=@5 , HangSx=@6  WHERE MaSanPham=@7", sp.TenSanPham, sp.Gia, sp.GiamGia, sp.TenHinh, sp.MoTa, sp.LoaiSp, sp.HangSx, sp.MaSanPham);
                }
            }
           
        }

        public static void DeleteProduct(int id)
        {
            try
            {
                using (var db = new ShopConnectionDB())
                {
                    db.Execute("delete from SanPham where MaSanPham = @0", id);
                }
            }
            catch
            {
            }
        }
    }
}