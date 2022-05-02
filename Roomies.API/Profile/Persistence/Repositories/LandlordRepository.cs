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
    public class LandlordRepository : BaseRepository, ILandlordRepository
    {
        public LandlordRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Landlord landlord)
        {
            await _context.Landlords.AddAsync(landlord);
        }

        public async Task<Landlord> FindById(int id)
        {
            //return await _context.Landlords.FindAsync(id);
            return await _context.Landlords.Include(l=>l.Plan).FirstAsync(l=>l.Id==id);
           // return await _context.Landlords.FirstAsync(l => l.Id == id);


        }

        public async Task<IEnumerable<Landlord>> ListAsync()
        {
            //            return await _context.Products.Include(p => p.Category).ToListAsync();

            return await _context.Landlords.Include(l=>l.Plan).ToListAsync();
        }

        public void Remove(Landlord landlord)
        {
            _context.Landlords.Remove(landlord);
        }

        public void Update(Landlord landlord)
        {
            _context.Landlords.Update(landlord);
        }
    }
}
