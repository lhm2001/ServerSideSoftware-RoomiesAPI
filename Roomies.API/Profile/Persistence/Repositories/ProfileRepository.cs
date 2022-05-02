using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Persistence.Repositories
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Profile user)
        {
            await _context.Profiles.AddAsync(user);
        }

        public async Task<Profile> FindById(int id)
        {
            //return await _context.Profiles.FindAsync(id).Include(p => p.Plan);
            //return await _context.Profiles.Include(p => p.Plan).Include(p => p.User).FirstAsync(p => p.Id == id);
            return await _context.Profiles.Include(p => p.Plan).FirstAsync(p => p.Id == id);

        }

        public async Task<Profile> FindByUserId(int userId)
        {
            return await _context.Profiles.Include(p => p.Plan).FirstAsync(p => p.UserId == userId);

        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await _context.Profiles.Include(p=>p.Plan).Include(p=>p.User).ToListAsync();
            //return await _context.Profiles.Include(p => p.Plan).Include(p=>p.User).ToListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByPlanId(int planId)
        {
            return await _context.Profiles
                .Where(p => p.PlanId == planId)
                .Include(p => p.Plan)
                .ToListAsync();
        }

        public void Remove(Profile user)
        {
            _context.Profiles.Remove(user);
        }

        public void Update(Profile user)
        {
            _context.Profiles.Update(user);
        }
    }
}
