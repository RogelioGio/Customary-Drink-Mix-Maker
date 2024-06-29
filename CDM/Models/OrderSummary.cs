using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDM.Models
{
    public class CashierReceipt
    {
        public int payment { get; set; }
        public List<string> drinks { get; set; }
        public int TotalCost { get; set; }
        public int Change { get; set; }
    }
    public class OrderItem
    {
        public string Drink { get; set; }
        public int Quantity { get; set; }
        public int TotalCost { get; set; }
        public string imgString {  get; set; }
    }
    public class OrderSummary
    {
        public List<string> SelectedDrinks { get; set; } = new List<string>();
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public List<int> Quantities { get; set; } = new List<int>();
        public int TotalCost { get; set; }
        public int Payment { get; set; }
        public int Change { get; set; }
        public string QueueNum { get; set; }

        public List<ProductTB> product { get; set; }
        public List<ProductTB> addon { get; set; }
        public ProductTB Products { get; set; }
    }

    
    
}