using System.Collections.Generic;
using System.Linq;

namespace HW_26.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(m => m.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Product product) => lineCollection.RemoveAll(m => m.Product.ProductId == product.ProductId);
        
        public decimal SumOFProduct() => lineCollection.Sum(m => m.Product.Price * m.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public IEnumerable<CartLine> Lines => lineCollection;
        
    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
