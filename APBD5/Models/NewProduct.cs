using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Models
{
    public class NewProduct
    {
        [Required(ErrorMessage = "IdProduct is required")]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "IdWarehouse is required")]
        public int IdWarehouse { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "CreatedAt is required")]
        public DateTime CreatedAt { get; set; }
    }
}
