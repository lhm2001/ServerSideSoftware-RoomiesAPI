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
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public async Task<Review> FindById(int id)
        {
            //            return await _context.Landlords.Include(l=>l.Plan).FirstAsync(l=>l.Id==id);

            return await _context.Reviews.Include(r => r.Post).Include(r => r.Leaseholder).FirstAsync(l => l.Id == id);

        }

        public async Task<IEnumerable<Review>> ListAsync()
        {
            return await _context.Reviews.Include(r => r.Post).Include(r=>r.Leaseholder).ToListAsync();
        }


        public async Task<IEnumerable<Review>> ListByLeaseholderId(int leaseholderId)
        {
            return await _context.Reviews
               .Where(p => p.LeaseholderId == leaseholderId)
               .Include(p => p.Leaseholder)
               .ToListAsync();
        }

        public async Task<IEnumerable<Review>> ListByPostId(int postId)
        {
            return await _context.Reviews
                .Where(p => p.PostId == postId)
                .Include(p => p.Post)
                .Include(p=>p.Leaseholder)
                .ToListAsync();
        }

        public void Remove(Review review)
        {
            _context.Reviews.Remove(review);
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
        }
    }
}
