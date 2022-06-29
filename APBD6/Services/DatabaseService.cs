using cw7.Models;
using cw7.Models.DTOs.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Services
{
    public class DatabaseService : IDbService
    {
        private readonly s20296Context _context;

        public DatabaseService(s20296Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<dynamic>> getClientsAsync()
        {
            var clients = await _context.Trips.Include(c => c.CountryTrips)
                                  .ThenInclude(c => c.IdCountryNavigation)
                                  .Include(e => e.ClientTrips)
                                  .ThenInclude(e => e.IdClientNavigation)
                                  .Select(c => new
                                  {
                                      Name = c.Name,
                                      Description = c.Description,
                                      DateFrom = c.DateFrom,
                                      DateTo = c.DateTo,
                                      MaxPeople = c.MaxPeople,
                                      Countries = c.CountryTrips.Select(e => new
                                      {
                                          e.IdCountryNavigation.Name
                                      }),
                                      Clients = c.ClientTrips.Select(c => new
                                      {
                                          c.IdClientNavigation.FirstName,
                                          c.IdClientNavigation.LastName
                                      })

                                  }).OrderByDescending(t => t.DateFrom).ToListAsync();
            return clients;
        }
        public async Task<bool> DeleteClientAsync(int idClient)
        {
            var findClient = FindClient(idClient);
            if (findClient == true) return false;
            var client = _context.Clients.First(c => c.IdClient == idClient);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;


        }
        public bool FindClient(int idClient)
        {
            return _context.Clients.Where(c => c.IdClient == idClient).Select(n => new
            {
                Nazwisko = n.FirstName,
                Liczba = n.ClientTrips.Count()
            }).Where(n => n.Liczba > 0).Any();
        }
        public async Task<bool> PostClientAsync(int idTrip, InsertClientDtoRequest clientRequest)
        {

            var client = _context.Clients.Where(c => c.Pesel == clientRequest.Pesel).Any();
            int clientIdTmp = -1;
            if (client == false)
            {
                var newClient = new Client
                {
                    IdClient = _context.Clients.Max(c => c.IdClient) + 1,
                    FirstName = clientRequest.FirstName,
                    LastName = clientRequest.LastName,
                    Email = clientRequest.Email,
                    Pesel = clientRequest.Pesel,
                    Telephone = clientRequest.Telephone
                };
                clientIdTmp = newClient.IdClient;
                _context.Clients.Add(newClient);
                await _context.SaveChangesAsync();
            }
            var clientTripExist = _context.Clients.Where(client => client.Pesel == clientRequest.Pesel)
                             .Include(client => client.ClientTrips.Where(clientTrips => clientTrips.IdTrip == clientRequest.IdTrip)).First();
            if (clientTripExist.ClientTrips.Count() > 0) return false;

            var tripExist = _context.Trips.Where(c => c.IdTrip == clientRequest.IdTrip).Any();
            if (tripExist == false) return false;
            if (clientIdTmp == -1)
            {
                clientIdTmp = _context.Clients.Where(c => c.Pesel == clientRequest.Pesel).Select(c => c.IdClient).First();
            }
            var newClientTrips = new ClientTrip
            {
                IdClient = clientIdTmp,
                IdTrip = clientRequest.IdTrip,
                PaymentDate = clientRequest.PaymentDate,
                RegisteredAt = DateTime.Now
            };
            _context.ClientTrips.Add(newClientTrips);
            await _context.SaveChangesAsync();

            return true;

        }

    }
}
