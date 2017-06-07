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
    }
}