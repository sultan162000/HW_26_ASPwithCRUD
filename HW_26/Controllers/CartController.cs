using System.Linq;
using HW_26.Infrastructure;
using HW_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_26.Controllers
{
    public class CartController : Controller
    {
        private readonly DateDb db;
        private Cart cart;
        public CartController(DateDb context,Cart cart)
        {
            db = context;
            this.cart = cart;
        }


        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewМodel { Cart = GetCart(), ReturnUrl = returnUrl });
        }


        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(m => m.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product,1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(m => m.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index",  new { returnUrl });
        }


    }
}