using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
using PetaPoco;
namespace _1461467DAWEB.Areas.Admin.Models
{
    public class AspNetUsersModel
    {
        public static Page<View_ChucVu> ListAccount(int pageNumber, int itemPage)
        {
            var db = new ShopConnectionDB();
            return db.Page<View_ChucVu>(pageNumber, itemPage, "select * from View_ChucVu");
        }

        public static IEnumerable<View_ChucVu> ChucVu(String id)
        {
            var db = new ShopConnectionDB();
            return db.Query<View_ChucVu>("select * from View_ChucVu where Id= @0", id);
        } 
        public static AspNetRole Roles(String chucvu) {
            var db = new ShopConnectionDB();
            return db.SingleOrDefault<AspNetRole>("select * from AspNetRoles where Name=@0 ", chucvu);
        }
        public static void UpdateRoles(String UserID, String RolesID)
        {
            var db = new ShopConnectionDB();
            AspNetUserRole Role = new AspNetUserRole();
            Role.UserId = UserID;
            Role.RoleId = RolesID;
            db.Update(Role);
        }
    }
}