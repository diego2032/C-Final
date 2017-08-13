using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualFinalProject.Models
{
    public class CartModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserID { get; set; }
        public int OrderTypeID { get; set; }
        public List<OrderDetail> CartItems { get; set; }
    }
}