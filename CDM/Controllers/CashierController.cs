using CDM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;

namespace CDM.Controllers
{
    public class CashierController : Controller
    {
        MixTillYouMakeIt db = new MixTillYouMakeIt();

        //Input CustomerQueueNum

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Search(string searchItem) {

            var order = from o in db.OrderTBs
                        select o;



            if (!String.IsNullOrEmpty(searchItem))
            {
                // Convert searchItem to string (assuming OrderQueueNum is an int)
                string searchNum = searchItem.Trim(); // Trim whitespace

                // Filter based on the numeric value converted to string
                order = order.Where(s => s.OrderQueueNum.ToString().Contains(searchNum));
            }


            return View(order.ToList());
        }

        //Transaction
        public ActionResult Receipt(FormCollection form, IEnumerable<string> word)
        {
            string paymentstring = form["Payment"];
            int pay = Convert.ToInt32(paymentstring);
            string Order = form["orderQueueNum"];

            var order = (from o in db.OrderTBs
                         where Order == null || o.OrderQueueNum.ToString().Contains(Order)
                         select o).SingleOrDefault();



            return View();
           


        }
    }
}
