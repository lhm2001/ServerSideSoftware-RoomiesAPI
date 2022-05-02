using Roomies.API.Domain.Models;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Services
{
    public class ProfilePaymentMethodService : IProfilePaymentMethodService
    {
        private readonly IProfilePaymentMethodRepository _profilePaymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfilePaymentMethodService(IProfilePaymentMethodRepository userPaymentMethodRepository, IUnitOfWork unitOfWork)
        {
            _profilePaymentMethodRepository = userPaymentMethodRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<ProfilePaymentMethodResponse> AssignProfilePaymentMethodAsync(int userId, int paymentMethodId)
        {
            try
            {
                await _profilePaymentMethodRepository.AssignProfilePaymentMethodAsync(userId, paymentMethodId);
                await _unitOfWork.CompleteAsync();
                ProfilePaymentMethod userPaymentMethod = await _profilePaymentMethodRepository.FindByUserIdAndPaymentMethodId(userId, paymentMethodId);
                return new ProfilePaymentMethodResponse(userPaymentMethod);

            }
            catch (Exception ex)
            {
                return new ProfilePaymentMethodResponse($"Un error ocurrió al asignar usuario y método de pago: {ex.Message}");
            }
        }


        public async Task<IEnumerable<ProfilePaymentMethod>> ListAsync()
        {
            return await _profilePaymentMethodRepository.ListAsync();
        }

        public async Task<IEnumerable<ProfilePaymentMethod>> ListByPaymentMethodIdAsync(int paymentMethodId)
        {
            return await _profilePaymentMethodRepository.ListByPaymentMethodIdAsync(paymentMethodId);
        }

        public async Task<IEnumerable<ProfilePaymentMethod>> ListByProfileIdAsync(int userId)
        {
            return await _profilePaymentMethodRepository.ListByProfileIdAsync(userId);
        }

        public async Task<ProfilePaymentMethodResponse> UnassignProfilePaymentMethodAsync(int userId, int paymentMethodId)
        {
            try
            {
                ProfilePaymentMethod userPaymentMethod = await _profilePaymentMethodRepository.FindByUserIdAndPaymentMethodId(userId, paymentMethodId);

                _profilePaymentMethodRepository.Remove(userPaymentMethod);
                await _unitOfWork.CompleteAsync();

                return new ProfilePaymentMethodResponse(userPaymentMethod);

            }
            catch (Exception ex)
            {
                return new ProfilePaymentMethodResponse($"Un error ocurrió al desasignar usuario y método de pago: {ex.Message}");
            }
        }
    }
}
