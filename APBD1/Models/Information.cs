using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APBD2.Models
{
    public class Information
    {
        public string createdAt { get; set; }
        public string author { get; set; }
        public HashSet<Student> studenci { get; set; }
        public Dictionary<string, int> activeStudies { get; set; }

        public void writer(List<string> fileContent)
        {
            var students = new HashSet<Student>(new OwnComparer());
            using var sw = File.AppendText("log.txt");
            var list = new List<String>();
            foreach (var line in fileContent)
            {

                var studentSplit = line.Split(",");
                var birth = DateTime.Parse(studentSplit[5]);
                Student student = (new Student
                {
                    indexNumber = "s" + studentSplit[4],
                    fname = studentSplit[0],
                    lname = studentSplit[1],
                    birthdate = birth.ToShortDateString(),
                    email = studentSplit[6],
                    mothersName = studentSplit[7],
                    fathersName = studentSplit[8],
                    studies = new University
                    {
                        name = studentSplit[2],
                        mode = studentSplit[3]
                    }
                }
                );
                if (students.Contains(student))
                {
                    string date = DateTime.Now.ToShortDateString();
                    sw.WriteLine(date + " " + line);
                }
                else if (String.IsNullOrEmpty(student.fname) || String.IsNullOrEmpty(student.lname) || String.IsNullOrEmpty(student.studies.name) || String.IsNullOrEmpty(student.studies.mode) || String.IsNullOrEmpty(student.indexNumber) || String.IsNullOrEmpty(student.email) || String.IsNullOrEmpty(student.fathersName) || String.IsNullOrEmpty(student.mothersName))
                {
                    string date = DateTime.Now.ToShortDateString();
                    sw.WriteLine(date + " " + line);
                }
                else students.Add(student);


            }
            sw.WriteLine();
            sw.Close();
            
            studenci = students;
            author = "Bartosz Wasilewski";
            createdAt = DateTime.Now.ToShortDateString();
        }

        public void studiesName()
        {
            Dictionary<string, int> counter = new Dictionary<string, int>();
            HashSet<string> hashSet = new HashSet<string>();
            foreach (var x in studenci)
            {
               hashSet.Add(x.studies.name);
            }


            int i = 0;
            foreach (var x in hashSet)
            {
                foreach (var y in studenci)
                {
                    if (x == y.studies.name)
                    {
                        i++;
                    }
                }

                counter.Add(x, i);
                i = 0;
            }
            activeStudies = counter;
        }

    }
}
