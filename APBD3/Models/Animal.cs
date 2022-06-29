using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Area is required")]
        public string Area { get; set; }

    }
}
