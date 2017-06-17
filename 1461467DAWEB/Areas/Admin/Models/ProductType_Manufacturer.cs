using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class ProductType_Manufacturer
    {
        public static Page<View_lsp_hsx> ListProduct(int pageNumber, int itemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<View_lsp_hsx>(pageNumber, itemPage, "select * from View_lsp_hsx");
        }

        public static IEnumerable<View_lsp_hsx> SearchAccount(String key)
        {
            var db = new ShopConnectionDB();
            return db.Fetch<View_lsp_hsx>("select * from View_lsp_hsx where TenSanPham LIKE @0", "%" + key + "%");
        }
    }
}