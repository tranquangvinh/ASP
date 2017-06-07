using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class AspNetUsersModel
    {
        public static IEnumerable<AspNetUser> ListAccount()
        {
            var db = new ShopConnectionDB();
            return db.Query<AspNetUser>("select * from AspNetUsers");
        }
    }
}