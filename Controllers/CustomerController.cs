using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MOweb.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View(new MOweb.Models.Customer());
        }

        [HttpPost]
        public IActionResult RsvpForm(MOweb.Models.Customer cust)
        {

            //return Redirect("~/Passport/List?Page=" + psp1view.PageNext.ToString() + "&customerid=" + psp1view.Customerid + "&host=" + psp1view.Host + "&key=" + psp1view.Key);  // https://localhost:44306/Passport/List?Page=1
            return View("CustomerSummary", new MOweb.Models.Customer(420958174,1));
            //asa}

        }

    }
}