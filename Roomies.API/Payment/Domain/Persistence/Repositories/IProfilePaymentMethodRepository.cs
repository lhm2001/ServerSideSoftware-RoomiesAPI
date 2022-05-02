using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IProfilePaymentMethodRepository
    {
        Task<IEnumerable<ProfilePaymentMethod>> ListAsync();
        Task<IEnumerable<ProfilePaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<ProfilePaymentMethod>> ListByProfileIdAsync(int profileId);
        void Remove(ProfilePaymentMethod profilePaymentMethod);
        Task AddAsync(ProfilePaymentMethod profilePaymentMethod);
        Task<ProfilePaymentMethod> FindByUserIdAndPaymentMethodId(int profileId, int paymentMethodId);
        Task AssignProfilePaymentMethodAsync(int profileId, int paymentMethodId);
        Task UnassignProfilePaymentMethodAsync(int profileId, int paymentMethodId);

    }
}
