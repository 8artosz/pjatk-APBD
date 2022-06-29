using FirsApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirsApi.Services
{
    public class DbService : IDbService
    {
        private static List<Student> students;

        public List<Student> GetStudent()
        {
            var fi = new FileInfo("Data/dane.csv");
            var fileContent = new List<string>();
            var student = new List<Student>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    fileContent.Add(line);
                }
            }
            foreach(var line in fileContent)
            {
                var studentSplit = line.Split(",");
                Student studentt = (new Student
                {
                    Imie = studentSplit[0],
                    Nazwisko = studentSplit[1],
                    NumerIndeksu = studentSplit[2],
                    DataUrodzenia = studentSplit[3],
                    Studia = studentSplit[4],
                    Tryb = studentSplit[5],
                    Email = studentSplit[6],
                    ImieOjca = studentSplit[7],
                    ImieMatki = studentSplit[8]
                });
                student.Add(studentt);
            }
            return student;
        }
        public Student GetStudent(string id)
        {
            Student studennt = null;
            var fi = new FileInfo("Data/dane.csv");
            var fileContent = new List<string>();
            var student = new List<Student>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    fileContent.Add(line);
                }
            }
            foreach (var line in fileContent)
            {
                var studentSplit = line.Split(",");
                Student studentt = (new Student
                {
                    Imie = studentSplit[0],
                    Nazwisko = studentSplit[1],
                    NumerIndeksu = studentSplit[2],
                    DataUrodzenia = studentSplit[3],
                    Studia = studentSplit[4],
                    Tryb = studentSplit[5],
                    Email = studentSplit[6],
                    ImieOjca = studentSplit[7],
                    ImieMatki = studentSplit[8]
                });
                student.Add(studentt);
            }
            foreach(var outputRe in student)
            {
                if(outputRe.NumerIndeksu == id)
                {
                    studennt = outputRe;
                }
            }
            return studennt;

        }
        public Student PutStudent (string i, Student s)
        {
            Student studennt = null;
            var fi = new FileInfo("Data/dane.csv");
            var fileContent = new List<string>();
            var student = new List<Student>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    fileContent.Add(line);
                }
            }
            foreach (var line in fileContent)
            {
                var studentSplit = line.Split(",");
                Student studentt = (new Student
                {
                    Imie = studentSplit[0],
                    Nazwisko = studentSplit[1],
                    NumerIndeksu = studentSplit[2],
                    DataUrodzenia = studentSplit[3],
                    Studia = studentSplit[4],
                    Tryb = studentSplit[5],
                    Email = studentSplit[6],
                    ImieOjca = studentSplit[7],
                    ImieMatki = studentSplit[8]
                });
                student.Add(studentt);
            }
            foreach (var outputRe in student)
            {
                if (outputRe.NumerIndeksu == s.NumerIndeksu)
                {
                    outputRe.Imie = s.Imie;
                    outputRe.Nazwisko = s.Nazwisko;
                    outputRe.NumerIndeksu = s.NumerIndeksu;
                    outputRe.DataUrodzenia = s.DataUrodzenia;
                    outputRe.Studia = s.Studia;
                    outputRe.Tryb = s.Tryb;
                    outputRe.Email = s.Email;
                    outputRe.ImieOjca = s.ImieOjca;
                    outputRe.ImieMatki = s.ImieMatki;

                }
            }
            using var sw = new StreamWriter("Data/Dane.csv");
            foreach(var output in student){
                sw.WriteLine(output.ToString());
            }
            sw.Close();
            return GetStudent(i);

        }
        public bool PostStudent (Student s)
        {
            Student studennt = null;
            var fi = new FileInfo("Data/dane.csv");
            var fileContent = new List<string>();
            var student = new List<Student>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    fileContent.Add(line);
                }
            }
            foreach (var line in fileContent)
            {
                var studentSplit = line.Split(",");
                Student studentt = (new Student
                {
                    Imie = studentSplit[0],
                    Nazwisko = studentSplit[1],
                    NumerIndeksu = studentSplit[2],
                    DataUrodzenia = studentSplit[3],
                    Studia = studentSplit[4],
                    Tryb = studentSplit[5],
                    Email = studentSplit[6],
                    ImieOjca = studentSplit[7],
                    ImieMatki = studentSplit[8]
                });
                student.Add(studentt);
            }
           
            foreach (var outputRe in student)
            {
                if (outputRe.NumerIndeksu == s.NumerIndeksu)
                {
                    return false;
                }
            }
            if (s.NumerIndeksu[0] != 's')
                return false;
            if (String.IsNullOrEmpty(s.Imie) || String.IsNullOrEmpty(s.Nazwisko) || String.IsNullOrEmpty(s.NumerIndeksu) || String.IsNullOrEmpty(s.DataUrodzenia) || String.IsNullOrEmpty(s.Studia) || String.IsNullOrEmpty(s.Tryb) || String.IsNullOrEmpty(s.Email) || String.IsNullOrEmpty(s.ImieOjca) || String.IsNullOrEmpty(s.ImieMatki))
                return false;
            StreamWriter sw = File.AppendText("Data/Dane.csv");
            sw.WriteLine(s.ToString());
            sw.Close();
            return true;
        }
        public bool DeleteStudent(string i)
        {
            Student studennt = null;
            var fi = new FileInfo("Data/dane.csv");
            var fileContent = new List<string>();
            var student = new List<Student>();
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    fileContent.Add(line);
                }
            }
            foreach (var line in fileContent)
            {
                var studentSplit = line.Split(",");
                Student studentt = (new Student
                {
                    Imie = studentSplit[0],
                    Nazwisko = studentSplit[1],
                    NumerIndeksu = studentSplit[2],
                    DataUrodzenia = studentSplit[3],
                    Studia = studentSplit[4],
                    Tryb = studentSplit[5],
                    Email = studentSplit[6],
                    ImieOjca = studentSplit[7],
                    ImieMatki = studentSplit[8]
                });
                student.Add(studentt);
            }
            int ii = -1;
            foreach (var outputRe in student)
            {
                ii++;
                if (outputRe.NumerIndeksu == i)
                {
                    student.RemoveAt(ii);
                    break;
                }
            }
            using var sw = new StreamWriter("Data/Dane.csv");
            foreach (var output in student)
            {
                sw.WriteLine(output.ToString());
            }
            sw.Close();
          

            return true;
        }

    }
}
