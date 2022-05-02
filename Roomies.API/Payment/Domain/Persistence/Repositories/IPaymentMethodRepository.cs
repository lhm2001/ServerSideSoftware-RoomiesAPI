using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> ListAsync();
        Task AddAsync(PaymentMethod paymentMethod);
        Task<PaymentMethod> FindById(int id);        
        void Remove(PaymentMethod paymentMethod);
        void Update(PaymentMethod paymentMethod);
    }
}
