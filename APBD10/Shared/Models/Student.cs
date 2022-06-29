using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw11.Shared.Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(100)]
        public string LastName { get; set; }
        public String Birthdate { get; set; }
        public String Studies { get; set; }
        public String Img { get; set; }
    }
}
