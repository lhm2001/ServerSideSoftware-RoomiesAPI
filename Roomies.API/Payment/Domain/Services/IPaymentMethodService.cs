using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task<IEnumerable<PaymentMethod>> ListByProfileIdAsync(int profileId);
        Task<PaymentMethodResponse> GetByIdAsync(int id);
        Task<PaymentMethodResponse> SaveAsync(PaymentMethod paymentMethod);
        Task<PaymentMethodResponse> DeleteAsync(int id);
    }
}
