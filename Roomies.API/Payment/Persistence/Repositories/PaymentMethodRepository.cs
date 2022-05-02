using Microsoft.EntityFrameworkCore;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Persistence.Repositories
{
    public class PaymentMethodRepository : BaseRepository, IPaymentMethodRepository
    {
        public PaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PaymentMethod paymentMethod)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);
        }

        public async Task<PaymentMethod> FindById(int id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public async Task<PaymentMethod> FindByIdAsync(int id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public void Remove(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
        }

        public void Update(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
        }
    }
}
