using cw11.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Server.Service
{
    public interface IDbService
    {
        public Task<List<Student>> GetStudentsAsync();
        public Task<Student> GetStudentAsync(int id);
        public Task<bool> DeleteStudentAsync(int id);
    }
}
