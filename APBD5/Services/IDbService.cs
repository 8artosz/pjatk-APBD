using cw5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Services
{
    public interface IDbService
    {
        public Task<int> IsCorrectValueAsync(NewProduct product);
        public Task<int> PostProductAsync(NewProduct product);
        public Task PostProductByStoredProcedureAsync(NewProduct product);
    }
}
