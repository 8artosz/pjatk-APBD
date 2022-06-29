using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD2.Models
{
    public class Output
    {
        public Information uczelnia { get; set; }

        public Output(Information information)
        {
            uczelnia = information;
        }
    }
}
