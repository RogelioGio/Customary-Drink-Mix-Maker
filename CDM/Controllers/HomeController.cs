using CDM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;


namespace CDM.Controllers
{
    public class HomeController : Controller
    {
        MixTillYouMakeIt db = new MixTillYouMakeIt();
        private static List<CustomDrink> customDrinks = new List<CustomDrink>();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new CustomerTB());
        }
        [HttpPost]
        public ActionResult Index(CustomerTB model)
        {

            Random r = new Random();

            var customer = new CustomerTB
            {
                Customer_ID = model.Customer_ID,
                CustomerNum = model.CustomerNum + "00" + r.Next(100, 999),
                Created_Date = DateTime.Now
            };

            db.CustomerTBs.Add(customer);
            db.SaveChanges();


            var records = new AdminRecord
            {
                Customer_ID = customer.Customer_ID
            };
            db.AdminRecords.Add(records);
            db.SaveChanges();

            if (model.CustomerNum == "OR")
            {
                return RedirectToAction("OrderDrink");
            }
            else
            {
                return RedirectToAction("MakeYourOwnDrink");
            }


        }

        //OrderProcesses
        [HttpGet]
        public ActionResult OrderDrink()
        {
            var Alcohol = db.ProductTBs.Where(p => p.Product_Category == "Alcohol").ToList();
            var Addon = db.ProductTBs.Where(p => p.Product_Category == "Add-on").ToList();
            var viewmodel = new OrderSummary
            {
                product = Alcohol,
                addon = Addon,
            };
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult OrderSummary(string[] selectedDrinks, int[] quantities, OrderSummary model2, OrderTB model)
        {
            Random r = new Random();
            string queueNum = r.Next(100, 999).ToString();
            var viewModel = new OrderSummary
            {
                OrderItems = new List<OrderItem>(),
                QueueNum = queueNum
            };



            for (int i = 0; i < selectedDrinks.Length; i++)
            {
                string drink = selectedDrinks[i];
                var select = db.ProductTBs.Where(p => p.Product_Name == drink).FirstOrDefault();
                int quantity = quantities[i];
                int totalCost = select.Product_Price * quantity;
                string order = drink + "(x" + quantity.ToString() + ")" + " - ₱" + totalCost;
                
                viewModel.OrderItems.Add(new OrderItem
                {
                    Drink = drink,
                    Quantity = quantity,
                    TotalCost = totalCost
                });

                viewModel.SelectedDrinks.Add(order);
                viewModel.TotalCost += totalCost;
            }

            var Order = new OrderTB
            {
                OrderDesc = String.Join(",", viewModel.SelectedDrinks),
                CreatedDate = DateTime.Now,
                CreatedTime = DateTime.Now.TimeOfDay
            };
            db.OrderTBs.Add(Order);
            db.SaveChanges();

            var data = db.AdminRecords.Where(x => x.ID == db.AdminRecords.Max(i => i.ID)).FirstOrDefault();
            if (data != null)
            {
                data.Order_ID = Order.Order_ID;
                db.SaveChanges();
            }

            TempData["OrderSummary"] = viewModel;
            return View(viewModel);
        }
        
        public ActionResult Receipt()
        {

            var viewModel = TempData["OrderSummary"] as OrderSummary;
            return View(viewModel);
        }



        //Customary Drink

        public ActionResult MakeYourOwnDrink()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MakeYourOwnDrink(FormCollection form)
        {
            Random r = new Random();
            string queueNum = r.Next(100, 999).ToString();
            string basedrink = form["BaseDrink"];
            var drink = new CustomDrink
            {
                BaseDrink = form["BaseDrink"],
                Size = form["Size"],
                Layers = new List<Layer>(),
                queue = queueNum
            };

            int layerCount = drink.Size == "Small" ? 3 : drink.Size == "Medium" ? 4 : 5;
            for (int i = 0; i < layerCount; i++)
            {
                string Shot = form[$"Layers[{i}].Shot"];
                var layer = new Layer
                {
                    Shot = form[$"Layers[{i}].Shot"],
                    Percentage = float.Parse(form[$"Layers[{i}].Percentage"])
                };
                drink.Layers.Add(layer);
            }

            for (int l = 0; l < drink.Layers.Count; l++)
            {
                drink.Layer.Add(drink.Layers[l].ToString());
            }

            
            drink.TotalCost = CalculateTotalCost(drink);
            customDrinks.Add(drink);


            return RedirectToAction("CustomOrderSummary");
        }

        public ActionResult CustomOrderSummary()
        {
            return View(customDrinks);
        }

        public ActionResult CustomReceipt(CustomDrink model)
        {
            try {
                List<string> drinkStrings = new List<string>();
                for (int i = 0; i < customDrinks.Count; i++)
                {
                    drinkStrings.Add("Drink" + (i + 1) + ":, " + customDrinks[i].ToString() + "Total: " + customDrinks[i].TotalCost);
                }

                var CustomDrinkOrder = new OrderTB
                {
                    OrderDesc = string.Join(", ", drinkStrings),
                    CreatedDate = DateTime.Now,
                    CreatedTime = DateTime.Now.TimeOfDay
                };
                db.OrderTBs.Add(CustomDrinkOrder);
                db.SaveChanges();

                var id = db.OrderTBs.Where(x => x.Order_ID == db.OrderTBs.Max(i => i.Order_ID)).FirstOrDefault().Order_ID;
                var data = db.AdminRecords.Where(x => x.ID == db.AdminRecords.Max(i => i.ID)).FirstOrDefault();
                if (data != null)
                {
                    data.Order_ID = id;
                    db.SaveChanges();
                }
            }
            

            catch (DbEntityValidationException e)
{
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View(customDrinks);

        }


        [HttpPost]
        public ActionResult ClearDrinks()
        {
            customDrinks.Clear();
            return RedirectToAction("Index");
        }

        private int CalculateTotalCost(CustomDrink drink)
        {
            int baseCost = db.ProductTBs.Where(p => p.Product_Name == drink.BaseDrink).Where(p => p.Product_Category == "Base").FirstOrDefault().Product_Price;
            int layersCost = drink.Layers.Sum(layer => db.ProductTBs.Where(p => p.Product_Name == layer.Shot).Where(p => p.Product_Category == "Shot").FirstOrDefault().Product_Price);
            return baseCost + layersCost;
        }

       


        [HttpGet]
        public ActionResult OrderSample()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderSample(OrderTB model)
        {
            db.OrderTBs.Add(model);
            return View(model);
        }
    }
}