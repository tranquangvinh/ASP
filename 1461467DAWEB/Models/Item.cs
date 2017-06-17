using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopConnection;
namespace _1461467DAWEB.Models
{
    public class Item
    {
        public SanPham sp = new SanPham();
        public int quantity;

        public void setSP(SanPham sanpham)
        {
            sp = sanpham;
        }
        public void setQuantity(int sl)
        {
            quantity = sl;
        }
        public SanPham GetSP()
        {
            return sp;
        }
        public int GetSL()
        {
            return quantity;
        }
        public Item(SanPham sanpham, int sl)
        {
            sp = sanpham;
            quantity = sl;
        }
    }
}