using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class HangSanXuatBus
    {
        public static IEnumerable<HangSanXuat> DanhSach()
        {
            var db = new ShopConnectionDB();
            return db.Query<HangSanXuat>("select * from HangSanXuat");
        }
    }
}