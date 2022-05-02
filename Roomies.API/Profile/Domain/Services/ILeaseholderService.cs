using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface ILeaseholderService
    {
        Task<IEnumerable<Leaseholder>> ListAsync();
        Task<IEnumerable<Leaseholder>> ListByPostIdAsync(int postId);
        Task<LeaseholderResponse> GetByIdAsync(int id);
        Task<LeaseholderResponse> SaveAsync(Leaseholder landlord,int planId, int userId);
        Task<LeaseholderResponse> UpdateAsync(int id, Leaseholder landlord);
        Task<LeaseholderResponse> DeleteAsync(int id);
    }
}
