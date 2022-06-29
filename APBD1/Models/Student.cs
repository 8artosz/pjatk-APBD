using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD2.Models
{
    public class Student
    {
        public string indexNumber { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public University studies { get; set; }
    }
}
