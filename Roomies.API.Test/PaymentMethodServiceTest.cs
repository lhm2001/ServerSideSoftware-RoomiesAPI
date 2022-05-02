using NUnit.Framework;
using Moq;
using FluentAssertions;
using Roomies.API.Domain.Repositories;
using Roomies.API.Services;
using Roomies.API.Domain.Services.Communications;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roomies.API.Domain.Models;

namespace Roomies.API.Test
{
    public class PaymentMethodServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoPaymentMethodReturnsEmptyCollection()
        {
            // Arrange

            var mockPaymentMethodRepository = GetDefaultIPaymentMethodRepositoryInstance();
            var mockProfilePaymentMethodRepository = GetDefaultIProfilePaymentMethodRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockPaymentMethodRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<PaymentMethod>());

            var service = new PaymentMethodService(mockPaymentMethodRepository.Object, mockProfilePaymentMethodRepository.Object, mockUnitOfWork.Object);

            // Act

            List<PaymentMethod> result = (List<PaymentMethod>)await service.ListAsync();
            var paymentMethodCount = result.Count;

            // Assert

            paymentMethodCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsPaymentMethodNotFoundResponse()
        {
            // Arrange
            var mockPaymentMethodRepository = GetDefaultIPaymentMethodRepositoryInstance();
            var mockProfilePaymentMethodRepository = GetDefaultIProfilePaymentMethodRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var paymentMethodId = 1;
            PaymentMethod paymentMethod = new PaymentMethod();
            mockPaymentMethodRepository.Setup(r => r.FindById(paymentMethodId)).Returns(Task.FromResult<PaymentMethod>(null));
            var service = new PaymentMethodService(mockPaymentMethodRepository.Object, mockProfilePaymentMethodRepository.Object, mockUnitOfWork.Object);

            // Act
            PaymentMethodResponse result = await service.GetByIdAsync(paymentMethodId);
            var message = result.Message;

            // Assert
            message.Should().Be("Medio de Pago inexistente");
        }

        private Mock<IPaymentMethodRepository> GetDefaultIPaymentMethodRepositoryInstance()
        {
            return new Mock<IPaymentMethodRepository>();
        }

        private Mock<IProfilePaymentMethodRepository> GetDefaultIProfilePaymentMethodRepositoryInstance()
        {
            return new Mock<IProfilePaymentMethodRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}