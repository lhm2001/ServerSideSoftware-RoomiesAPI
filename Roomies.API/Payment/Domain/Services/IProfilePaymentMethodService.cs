using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IProfilePaymentMethodService
    {
        Task<IEnumerable<ProfilePaymentMethod>> ListAsync();
        Task<IEnumerable<ProfilePaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId);
        Task<IEnumerable<ProfilePaymentMethod>> ListByProfileIdAsync(int profileId);
        Task<ProfilePaymentMethodResponse> AssignProfilePaymentMethodAsync(int profileId, int paymentMethodId);
        Task<ProfilePaymentMethodResponse> UnassignProfilePaymentMethodAsync(int profileId, int paymentMethodId);
    }
}