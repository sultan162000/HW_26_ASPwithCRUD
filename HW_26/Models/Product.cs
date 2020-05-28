using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace HW_26.Models
{
    public class Product 
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Имя - обязательно поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Цена - обязательно поле")]
        public decimal Price { get; set; }
        
        public virtual Category Category { get; set; }
        
    }
}
