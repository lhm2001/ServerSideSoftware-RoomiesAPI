using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Persistence.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> ListAsync();
        Task<IEnumerable<Profile>> ListByPlanId(int planId);
        Task<Profile> FindById(int id);
        Task<Profile> FindByUserId(int userId);
        Task AddAsync(Profile user);
        void Update(Profile user);
        void Remove(Profile user);
    }
}
