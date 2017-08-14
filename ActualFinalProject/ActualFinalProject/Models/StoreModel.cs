using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ActualFinalProject.Models
{
    public class StoreModel
    {
        public List<Item> Items { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public List<ItemCategory> Categories { get; set; }
    }
}