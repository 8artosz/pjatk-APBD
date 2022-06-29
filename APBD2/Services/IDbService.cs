using FirsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirsApi.Services
{
    public interface IDbService
    {
        public List<Student> GetStudent();
        public Student GetStudent(string i);
        public Student PutStudent(string id, Student s);
        public bool PostStudent(Student s);
        public bool DeleteStudent(string i);


    }
}
