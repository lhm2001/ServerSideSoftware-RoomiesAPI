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
    public class ProfilePaymentMethodRepository : BaseRepository, IProfilePaymentMethodRepository
    {
        public ProfilePaymentMethodRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ProfilePaymentMethod userPaymentMethod)
        {
            await _context.UserPaymentMethods.AddAsync(userPaymentMethod);
        }

        public async Task AssignProfilePaymentMethodAsync(int profileId, int paymentMethodId)
        {
            ProfilePaymentMethod userPaymentMethod = await FindByProfileIdAndPaymentMethodId(profileId, paymentMethodId);
            if (userPaymentMethod == null)
            {
                userPaymentMethod = new ProfilePaymentMethod { ProfileId = profileId, PaymentMethodId = paymentMethodId };
                await AddAsync(userPaymentMethod);
            }
        }

        public async Task<ProfilePaymentMethod> FindByProfileIdAndPaymentMethodId(int userId, int paymentMethodId)
        {
            return await _context.UserPaymentMethods.FindAsync(userId, paymentMethodId);
        }

        public Task<ProfilePaymentMethod> FindByUserIdAndPaymentMethodId(int profileId, int paymentMethodId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProfilePaymentMethod>> ListAsync()
        {
            return await _context.UserPaymentMethods.ToListAsync();
        }

        public async Task<IEnumerable<ProfilePaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _context.UserPaymentMethods
               .Where(pt => pt.PaymentMethodId == paymentMethodId)
               .Include(pt => pt.PaymentMethod)
               .Include(pt => pt.Profile)
               .ToListAsync();
        }

        public async Task<IEnumerable<ProfilePaymentMethod>> ListByProfileIdAsync(int profileId)
        {
            return await _context.UserPaymentMethods
               .Where(pt => pt.ProfileId == profileId)
               .Include(pt => pt.PaymentMethod)
               .Include(pt => pt.Profile)
               .ToListAsync();
        }

        public void Remove(ProfilePaymentMethod userPaymentMethod)
        {
            _context.UserPaymentMethods.Remove(userPaymentMethod);
        }

        public async Task UnassignProfilePaymentMethodAsync(int userId, int paymentMethodId)
        {
            ProfilePaymentMethod userPaymentMethod = await FindByProfileIdAndPaymentMethodId(userId, paymentMethodId);
            if (userPaymentMethod != null)
                Remove(userPaymentMethod);
        }
    }
}
