using CDM.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Util;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace CDM.Controllers
{
    public class AdminController : Controller
    {
        MixTillYouMakeIt db = new MixTillYouMakeIt();

        //Index Admin
        [HttpGet]
        public ActionResult Index()
        {

            var listofdata = new AdminRecordViewModel
            {
                AdminRecords = db.AdminRecords.ToList(),
                ProductTB = db.ProductTBs.ToList(),
                OrderTB = db.OrderTBs.Where(o => o.OrderQueueNum != null).ToList()
            };

            // Return the same view
            return View(listofdata);

        }

        //Detail
        public ActionResult Details(int ID) {

            var id = db.AdminRecords.Where(a => a.ID == ID).FirstOrDefault();

            CustomerTB customer = null;
            OrderTB order = null;
            TransactionTB transaction = null
                ;
            if (id != null) {
                customer = db.CustomerTBs.FirstOrDefault(c => c.Customer_ID == id.Customer_ID);
                order = db.OrderTBs.FirstOrDefault(o => o.Order_ID == id.Order_ID);
                transaction = db.TransactionTBs.FirstOrDefault(t => t.TransactionID == id.Transaction_ID);
            }

            /*var viewmodel = new OrderTransactionCustomerViewModel
            (
                customer?.Customer_ID ?? 0,
                customer?.CustomerNum ?? string.Empty,
                customer?.Created_Date ?? DateTime.MinValue,
                order?.Order_ID ?? 0,
                order?.OrderDesc ?? string.Empty,
                order?.CreatedDate ?? DateTime.MinValue,
                order?.CreatedTime ?? TimeSpan.Zero,
                order?.OrderQueueNum ?? string.Empty,
                order?.TotalCost ?? 0,
                transaction?.TransactionID ?? 0,
                transaction?.Total_Transaction ?? 0,
                transaction?.Payement_Amount ?? 0,
                transaction?.Change ?? 0
           );*/


            return View();


        }

        //Edit
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var id = db.AdminRecords.Where(a => a.ID == ID).FirstOrDefault();

            if (id == null)
            {
                return HttpNotFound();
            }

            var customer = db.CustomerTBs.FirstOrDefault(c => c.Customer_ID == id.Customer_ID);
            var order = db.OrderTBs.FirstOrDefault(o => o.Order_ID == id.Order_ID);
            var transaction = db.TransactionTBs.FirstOrDefault(t => t.TransactionID == id.Transaction_ID);

            /*var viewmodel = new OrderTransactionCustomerViewModel
            (
                customer?.Customer_ID ?? 0,
                customer?.CustomerNum ?? string.Empty,
                customer?.Created_Date ?? DateTime.MinValue,
                order?.Order_ID ?? 0,
                order?.OrderDesc ?? string.Empty,
                order?.CreatedDate ?? DateTime.MinValue,
                order?.CreatedTime ?? TimeSpan.Zero,
                order?.OrderQueueNum ?? string.Empty,
                order?.TotalCost ?? 0,
                transaction?.TransactionID ?? 0,
                transaction?.Total_Transaction ?? 0,
                transaction?.Payement_Amount ?? 0,
                transaction?.Change ?? 0
            );*/

            return View();

        }

        public ActionResult Edit(OrderTransactionCustomerViewModelEdit model)
        {
            var customer = db.CustomerTBs.Where(c => c.Customer_ID == model.customer.Customer_ID).FirstOrDefault();
            var order = db.OrderTBs.Where(o => o.Order_ID == model.order.Order_ID).FirstOrDefault();
            var transaction = db.TransactionTBs.Where(t => t.TransactionID == model.transaction.TransactionID).FirstOrDefault();

            if (customer != null)
            {
                //edit customer
                customer.CustomerNum = model.customer.CustomerNum;
                customer.Created_Date = model.customer.Created_Date;
            }


            /*if (order != null) {
                //edit order
                order.OrderDesc = model.order.OrderDesc;
                order.CreatedDate = model.order.CreatedDate;
                order.CreatedTime = model.order.CreatedTime;
                order.TotalCost = model.order.TotalCost;
            }

            if (transaction != null) {
                //edit transaction
                transaction.Total_Transaction = model.transaction.Total_Transaction;
                transaction.Payement_Amount = model.transaction.Payement_Amount;
                transaction.Change = model.transaction.Change;
            }
            db.SaveChanges();*/
            return RedirectToAction("Index");
        }

        //delete
        public ActionResult Delete(int ID)
        {
            var id = db.AdminRecords.Where(a => a.ID == ID).FirstOrDefault();
            var customer = db.CustomerTBs.FirstOrDefault(c => c.Customer_ID == id.Customer_ID);
            var order = db.OrderTBs.FirstOrDefault(o => o.Order_ID == id.Order_ID);
            var transaction = db.TransactionTBs.FirstOrDefault(t => t.TransactionID == id.Transaction_ID);

            db.AdminRecords.Remove(id);
            if (customer != null)
            {
                var customerID = db.CustomerTBs.Where(c => c.Customer_ID == customer.Customer_ID).FirstOrDefault();
                db.CustomerTBs.Remove(customerID);
            }

            if (order != null)
            {
                var orderID = db.OrderTBs.Where(o => o.Order_ID == order.Order_ID).FirstOrDefault();
                db.OrderTBs.Remove(orderID);
            }

            if (transaction != null) {
                var transactionID = db.TransactionTBs.Where(t => t.TransactionID == transaction.TransactionID).FirstOrDefault();
                db.TransactionTBs.Remove(transactionID);
            }


            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //Create Drink Entry
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductTB model)
        {
            db.ProductTBs.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Total Sales
        public ActionResult TotalSales(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null)
            {
                fromDate = DateTime.Now;
            }
            if (toDate == null)
            {
                toDate = DateTime.Now;
            }


            /*var listofData = new AdminRecordViewModel
            {
                order = db.OrderTBs.Where(o => (!fromDate.HasValue || o.CreatedDate >= fromDate) &&
                                                           (!toDate.HasValue || o.CreatedDate <= toDate)).ToList()

            };*/


            return View();
        }



        //Showtable
        public ActionResult Customer()
        {
            var listofdata = db.CustomerTBs.ToList();
            return View(listofdata);
        }

        public ActionResult Order()
        {
            var listofdata = db.OrderTBs.ToList();
            return View(listofdata);

        }

        public ActionResult Transaction()
        {
            var listofdata = db.TransactionTBs.ToList();
            return View(listofdata);
        }

        public ActionResult Product()
        {
            var listofdata = db.ProductTBs.ToList();
            return View(listofdata);
        }


        //edit_Customer
        [HttpGet]
        public ActionResult CEdit(int Customer_ID)
        {

            var viewmodel = db.CustomerTBs.Where(c => c.Customer_ID == Customer_ID).FirstOrDefault();
            return View(viewmodel);

        }
        [HttpPost]
        public ActionResult CEdit(CustomerTB model)
        {
            var viewmodel = db.CustomerTBs.Where(c => c.Customer_ID == model.Customer_ID).FirstOrDefault();
            if (viewmodel != null) 
            {
                viewmodel.CustomerNum = model.CustomerNum;
                viewmodel.Created_Date = DateTime.Now;
                db.SaveChanges();
            }
           ;
            return RedirectToAction("Index");
        }

        //details_Customer
        [HttpGet]
        public ActionResult CDetail(int Customer_ID)
        {

            var viewmodel = db.CustomerTBs.Where(c => c.Customer_ID == Customer_ID).FirstOrDefault();
            return View(viewmodel);

        }

        //delete_Customer
        public ActionResult CDelete(int Customer_ID) 
        {
            var data = db.CustomerTBs.Where(x => x.Customer_ID == Customer_ID).FirstOrDefault();
            var admin = db.AdminRecords.Where(x => x.Customer_ID == Customer_ID).FirstOrDefault();
            db.AdminRecords.Remove(admin);
            db.CustomerTBs.Remove(data);
            db.SaveChanges();
            ViewBag.Messsage = "Record Delete Successfully";
            return RedirectToAction("index");
        }

        //edit_Order
        [HttpGet]
        public ActionResult OEdit(int Order_ID)
        {

            var viewmodel = db.OrderTBs.Where(c => c.Order_ID == Order_ID).FirstOrDefault();
            return View(viewmodel);

        }
        [HttpPost]
        public ActionResult OEdit(OrderTB model)
        {
            var viewmodel = db.OrderTBs.Where(c => c.Order_ID == model.Order_ID ).FirstOrDefault();
            if (viewmodel != null)
            {
                viewmodel.OrderDesc = model.OrderDesc;
                viewmodel.CreatedDate = DateTime.Now;
                viewmodel.CreatedTime = DateTime.Now.TimeOfDay;
                /*viewmodel.TotalCost = model.TotalCost;*/
                db.SaveChanges();
            }
           ;
            return RedirectToAction("Index");
        }

        

    }

    
}
