using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW_26.Models
{
    public class Order
    {
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage ="Введите адрес доставки")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Введите номер телефона")]
        [RegularExpression(@"^[0-9]{1,9}$",ErrorMessage = "Введите адрес доставки")]
        public string Number { get; set; }

        
        public DateTime dateTime { get; set; }
    }
}
