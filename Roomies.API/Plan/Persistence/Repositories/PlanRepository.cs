using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Persistence.Repositories
{
    public class PlanRepository : BaseRepository, IPlanRepository
    {
        public PlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Domain.Models.Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }

        public async Task<Domain.Models.Plan> FindById(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Plan>> ListAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public void Remove(Domain.Models.Plan plan)
        {
            _context.Plans.Remove(plan);
        }

        public void Update(Domain.Models.Plan plan)
        {
            _context.Plans.Update(plan);
        }
    }
}
