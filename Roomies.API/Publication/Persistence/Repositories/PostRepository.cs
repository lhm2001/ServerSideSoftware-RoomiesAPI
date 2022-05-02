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
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
        }

        public async Task<Post> FindById(int postId)
        {
            //return await _context.Posts.FindAsync(postId);
            return await _context.Posts.Include(p => p.Landlord).FirstAsync(p => p.Id == postId);

        }

        public async Task<IEnumerable<Post>> ListAsync()
        {
            //return await _context.Posts.ToListAsync();
            return await _context.Posts.Include(p => p.Landlord).ToListAsync();
        }

        public async Task<IEnumerable<Post>> ListByLandlordIdAsync(int landlordId)
        {
            return await _context.Posts
                .Where(p => p.LandlordId == landlordId)
                .Include(p => p.Landlord)
                .ToListAsync();
        }

      
        public void Remove(Post post)
        {
            _context.Posts.Remove(post);
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
