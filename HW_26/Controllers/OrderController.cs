using HW_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_26.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View(new Order { });
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            return RedirectToAction("Thanks", "Order", order);
        }

        public IActionResult Thanks(Order order)
        {
            return View(order);
        }
    }
}