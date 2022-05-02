using Roomies.API.Publication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Domain.Persistence.Repositories
{
    public interface IRuleRepository
    {
        Task<IEnumerable<Rule>> ListAsync();
        Task<IEnumerable<Rule>> ListByPostId(int postId);
        Task<Rule> FindById(int id);
        Task AddAsync(Rule rule);
        void Update(Rule rule);
        void Remove(Rule rule);
    }
}
