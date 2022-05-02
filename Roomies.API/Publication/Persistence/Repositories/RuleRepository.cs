using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Persistence.Repositories;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Persistence.Repositories
{
    public class RuleRepository : BaseRepository, IRuleRepository
    {
        public RuleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Rule rule)
        {
            await _context.Rules.AddAsync(rule);
        }

        public async Task<Rule> FindById(int id)
        {
            return await _context.Rules.FindAsync(id);
        }

        public async Task<IEnumerable<Rule>> ListAsync()
        {
            return await _context.Rules.ToListAsync();
        }

        public async Task<IEnumerable<Rule>> ListByPostId(int postId)
        {
            return await _context.Rules
                .Where(r=>r.PostId==postId)
                .ToListAsync();
        }

        public void Remove(Rule rule)
        {
            _context.Rules.Remove(rule);
        }

        public void Update(Rule rule)
        {
            _context.Rules.Update(rule);
        }
    }
}
