using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATD2016.ApplicationParts.Controllers
{
    // Even though 'Order' is one of the public model types in the system, 
    // we won't use GenericController<Order> - this class takes precedence.
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return Content($"Pozdrav ATD publici sa NE-genericnog {GetType().Name} controllera.");
        }
    }
}
