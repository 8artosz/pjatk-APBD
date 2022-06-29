using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
