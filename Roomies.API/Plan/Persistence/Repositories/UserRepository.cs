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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> FindById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByUsername(string username)
        {
            IEnumerable<User> users = await ListAsync();
            var user = users.ToList().SingleOrDefault(x => x.Username == username);
            return user;

        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
