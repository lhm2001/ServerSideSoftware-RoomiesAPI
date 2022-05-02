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
    public class ReviewServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoReviewReturnsEmptyCollection()
        {
            // Arrange

            var mockReviewRepository = GetDefaultIReviewRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockReviewRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Review>());

            var service = new ReviewService(mockReviewRepository.Object, mockUnitOfWork.Object);

            // Act

            List<Review> result = (List<Review>)await service.ListAsync();
            var reviewCount = result.Count;

            // Assert

            reviewCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryNotFoundResponse()
        {
            // Arrange
            var mockReviewRepository = GetDefaultIReviewRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var reviewId = 1;
            Review review = new Review();
            mockReviewRepository.Setup(r => r.FindById(reviewId)).Returns(Task.FromResult<Review>(null));
            var service = new ReviewService(mockReviewRepository.Object, mockUnitOfWork.Object);

            // Act
            ReviewResponse result = await service.GetByIdAsync(reviewId);
            var message = result.Message;

            // Assert
            message.Should().Be("Review inexistente");
        }

        private Mock<IReviewRepository> GetDefaultIReviewRepositoryInstance()
        {
            return new Mock<IReviewRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}