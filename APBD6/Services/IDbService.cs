using cw7.Models;
using cw7.Models.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Services
{
    public interface IDbService
    {
        public Task<IEnumerable<dynamic>> getClientsAsync();
        public bool FindClient(int idClient);
        public Task <bool> DeleteClientAsync(int idClient);
        public Task<bool> PostClientAsync(int idTrip, InsertClientDtoRequest client);
    }
}
