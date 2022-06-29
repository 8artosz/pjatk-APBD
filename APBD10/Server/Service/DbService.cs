using cw11.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Server.Service
{
    public class DbService : IDbService
    {
        private static List<Student> _students = new List<Student>();
        static DbService()
        {
            _students.Add(new Student
            {
                IdStudent = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Birthdate = "1999/02/02",
                Studies = "Informatyka",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"

            });
            _students.Add(new Student
            {
                IdStudent = 2,
                FirstName = "Anna",
                LastName = "Malewska",
                Birthdate = "1999/12/12",
                Studies = "Informatyka",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 3,
                FirstName = "Andrzej",
                LastName = "Andrzejewski",
                Birthdate = "2000/06/12",
                Studies = "Geologia",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 4,
                FirstName = "Bartosz",
                LastName = "Wasilewski",
                Birthdate = "1999/02/10",
                Studies = "Informatyka",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 5,
                FirstName = "Anna",
                LastName = "Gogolewska",
                Birthdate = "2000/03/10",
                Studies = "Kosmetologia",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 7,
                FirstName = "Kamil",
                LastName = "Jaworski",
                Birthdate = "1998/10/10",
                Studies = "Dziennikarstwo",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 8,
                FirstName = "Kamila",
                LastName = "Roman",
                Birthdate = "2000/03/04",
                Studies = "Kosmetologia",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 8,
                FirstName = "Aleksander",
                LastName = "Kozlowski",
                Birthdate = "1999/08/02",
                Studies = "Dziennikarstwo",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 9,
                FirstName = "Maciek",
                LastName = "Maciejewski",
                Birthdate = "2000/03/12",
                Studies = "Geologia",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 10,
                FirstName = "Roman",
                LastName = "Kowalewski",
                Birthdate = "1999/04/18",
                Studies = "Informatyka",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
            _students.Add(new Student
            {
                IdStudent = 11,
                FirstName = "Nela",
                LastName = "Mikon",
                Birthdate = "2000/08/09",
                Studies = "Kosmetologia",
                Img = "https://cdn2.iconfinder.com/data/icons/professions/512/student_graduate_boy_profile-512.png"
            });
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return _students;
        }
        public async Task<Student> GetStudentAsync(int id)
        {
            Student tmp = new Student();
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].IdStudent == id)
                {
                    tmp = _students[i];
                }
                   
            }
            return tmp;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
         
           _students.Remove(_students.First(x => x.IdStudent == id));
            return true;

        }
    }
}
