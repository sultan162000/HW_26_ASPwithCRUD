using HW_26.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW_26.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;

        public CartSummaryViewComponent(Cart Cart)
        {
            cart = Cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
