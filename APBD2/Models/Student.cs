using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirsApi.Models
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerIndeksu { get; set; }
        public string DataUrodzenia { get; set; }
        public string Studia { get; set; }
        public string Tryb { get; set; }
        public string Email { get; set; }
        public string ImieOjca { get; set; }
        public string ImieMatki { get; set; }
        public string ToString()
        {
            string txt = "";
            txt = Imie + "," + Nazwisko + "," + NumerIndeksu + "," + DataUrodzenia + "," + Studia + "," + Tryb + "," + Email + "," + ImieOjca + "," + ImieMatki;
            return txt;
        }





    }
}
