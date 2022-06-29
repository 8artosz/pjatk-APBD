using kolokwium.DTOs.Requests;
using kolokwium.DTOs.Responses;
using kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium.Services
{
    public class DbService : IDbService
    {
        private readonly s20296Context _context;

        public DbService(s20296Context context)
        {
            _context = context;
        }

        public async Task<List<GetActionsAsyncDtoResponse>> GetActionsAsync(int idFirefighter)
        {
            var actions = await _context.Firefighters.Where(e => e.IdFirefighter == idFirefighter).Include(e => e.FirefighterActions).ThenInclude(e => e.IdActionNavigation).Select(n => n.FirefighterActions.Select(z => new GetActionsAsyncDtoResponse
            {
                IdAction= z.IdActionNavigation.IdAction,
                StartTime = z.IdActionNavigation.StartTime,
                EndTime = z.IdActionNavigation.EndTime
            }).OrderByDescending(e => e.EndTime).ToList()).FirstAsync();
            return actions;
        }
        public async Task<bool> PostFireTruckAsync(FireTruckDtoRequest firetruck)
        {
            var newFireTruckAction = new FireTruckAction
            {
               IdFireTruckAction = _context.FireTruckActions.Max(c => c.IdFireTruckAction) + 1,
               IdFireTruck = firetruck.IdFireTruck,
               IdAction = firetruck.IdAction,
               AssignmentDate = DateTime.Now
            };
            await _context.FireTruckActions.AddAsync(newFireTruckAction);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
