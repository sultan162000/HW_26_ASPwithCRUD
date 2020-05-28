using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW_26.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        public List<Product> Products { get; set; }
        public ProductList List { get; set; }

    }
    
    public enum ProductList
    {
        [Display(Name = "Молочное")]
        Milk,
        [Display(Name = "Фрукты")]
        Fruits,
        [Display(Name = "Мясное")]
        Meaty
    }
}
