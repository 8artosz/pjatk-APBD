using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8.DTOs.Requests
{
    public class PostDoctorDtoRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
