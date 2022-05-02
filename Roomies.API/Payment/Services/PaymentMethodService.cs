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
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IProfilePaymentMethodRepository _userPaymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodService(IPaymentMethodRepository paymentMethodRepository, IProfilePaymentMethodRepository userPaymentMethodRepository, IUnitOfWork unitOfWork)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _userPaymentMethodRepository = userPaymentMethodRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<PaymentMethodResponse> DeleteAsync(int id)
        {
            var existingPaymentMethod = await _paymentMethodRepository.FindById(id);

            if (existingPaymentMethod == null)
                return new PaymentMethodResponse("Medio de Pago inexistente");

            try
            {
                _paymentMethodRepository.Remove(existingPaymentMethod);
                await _unitOfWork.CompleteAsync();

                return new PaymentMethodResponse(existingPaymentMethod);
            }
            catch (Exception ex)
            {
                return new PaymentMethodResponse($"Un error ocurrió al buscar la conversación: {ex.Message}");
            }
        }

        public async Task<PaymentMethodResponse> GetByIdAsync(int id)
        {
            var existingPaymentMethod = await _paymentMethodRepository.FindById(id);

            if (existingPaymentMethod == null)
                return new PaymentMethodResponse("Medio de Pago inexistente");

            return new PaymentMethodResponse(existingPaymentMethod);
        }

        public async Task<IEnumerable<PaymentMethod>> ListAsync()
        {
            return await _paymentMethodRepository.ListAsync();
        }

        public async Task<IEnumerable<PaymentMethod>> ListByProfileIdAsync(int userId)
        {
            var userPaymentMethods = await _userPaymentMethodRepository.ListByProfileIdAsync(userId);
            var paymentMethod= userPaymentMethods.Select(pt => pt.PaymentMethod).ToList();
            return paymentMethod;
        }

        public async Task<PaymentMethodResponse> SaveAsync(PaymentMethod paymentMethod)
        {
            try
            {
                await _paymentMethodRepository.AddAsync(paymentMethod);
                await _unitOfWork.CompleteAsync();

                return new PaymentMethodResponse(paymentMethod);
            }
            catch (Exception ex)
            {
                return new PaymentMethodResponse($"Un error ocurrió al guardar el medio de pago: {ex.Message}");
            }
        }
    }
}
