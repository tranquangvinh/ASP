using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class ProductType_Manufacturer
    {
        public static IEnumerable<View_lsp_hsx> ListProduct()
        {
            var db = new ShopConnectionDB();
            return db.Query<View_lsp_hsx>("select * from View_lsp_hsx");
        }
    }
}