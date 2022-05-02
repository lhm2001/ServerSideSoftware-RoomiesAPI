using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface ILandlordService
    {
        Task<IEnumerable<Landlord>> ListAsync();
        Task<LandlordResponse> GetByIdAsync(int id);
        Task<LandlordResponse> SaveAsync(Landlord landlord,int planId, int userId);
        Task<LandlordResponse> UpdateAsync(int id, Landlord landlord);
        Task<LandlordResponse> DeleteAsync(int id);

    }
}
