using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CDM.Models
{
    public class OrderTransactionCustomerViewModel
    {
        //Customer
        public int Customer_ID { get; set; }
        public string CustomerNum { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }

        //Order
        public int Order_ID { get; set; }
        public string OrderDesc { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.TimeSpan> CreatedTime { get; set; }
        public string OrderQueueNum { get; set; }
        public Nullable<int> TotalCost { get; set; }

        //Transaction
        public int TransactionID { get; set; }
        public Nullable<int> Total_Transaction { get; set; }
        public Nullable<int> Payement_Amount { get; set; }
        public Nullable<int> Change { get; set; }

        public OrderTransactionCustomerViewModel(int customer_ID = 0, string customerNum = "N/A" , DateTime? created_Date = default, 
                                                 int order_ID = 0, string orderdesc = "N/A", DateTime? createdDate = default, TimeSpan? createdTime = default, string orderquenum = "N/A", int? totalcost = 0, 
                                                 int trasaction_ID = 0, int? totaltransaction = 0, int? payement = 0, int? change = 0)
        {
            Customer_ID = customer_ID;
            CustomerNum = customerNum;
            Created_Date = created_Date;
            Order_ID = order_ID;
            OrderDesc = orderdesc;
            CreatedDate = createdDate;
            CreatedTime = createdTime;
            OrderQueueNum = orderquenum;
            TotalCost = totalcost;
            TransactionID = trasaction_ID;
            Total_Transaction = totaltransaction;
            Payement_Amount = payement;
            Change = change;
            
        }
        
        public void SaveChanges(MixTillYouMakeIt db)
        {
            var customer = db.CustomerTBs.FirstOrDefault(c => c.Customer_ID == Customer_ID);
            customer.CustomerNum = CustomerNum;
            customer.Created_Date = (DateTime)CreatedDate;

            var order = db.OrderTBs.FirstOrDefault(o => o.Order_ID == Order_ID);
            order.OrderDesc = OrderDesc;
            order.CreatedDate = CreatedDate;
            

            var transaction = db.TransactionTBs.FirstOrDefault(t => t.TransactionID == TransactionID);
            transaction.Total_Transaction = Total_Transaction;
            transaction.Payement_Amount = Payement_Amount;
            transaction.Change = Change;
            
            db.SaveChanges();
        }
    }
    public class OrderTransactionCustomerViewModelEdit { 
        public OrderTB order { get; set; }
        public CustomerTB customer { get; set; }
        public TransactionTB transaction { get; set; }
    }
}