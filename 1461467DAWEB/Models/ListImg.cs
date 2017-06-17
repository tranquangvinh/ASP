using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class ListImg
    {
        public static IEnumerable<DanhSachHinh> DanhSachHinhSanPham(int id)
        {
            var db = new ShopConnectionDB();
            return db.Query<DanhSachHinh>("Select * from DanhSachHinh where IdHinh=@0", id);
        }
    }
}