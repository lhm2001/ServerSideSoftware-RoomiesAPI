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
    public class PostServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoPostReturnsEmptyCollection()
        {
            // Arrange

            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockFavouritePostRepository = GetDefaultIFavouritePostRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockPostRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Post>());

            var service = new PostService(mockPostRepository.Object, mockUnitOfWork.Object, mockFavouritePostRepository.Object);

            // Act

            List<Post> result = (List<Post>)await service.ListAsync();
            var postCount = result.Count;

            // Assert

            postCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryNotFoundResponse()
        {
            // Arrange
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockFavouritePostRepository = GetDefaultIFavouritePostRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var postId = 1;
            Post post = new Post();
            mockPostRepository.Setup(r => r.FindById(postId)).Returns(Task.FromResult<Post>(null));
            var service = new PostService(mockPostRepository.Object, mockUnitOfWork.Object, mockFavouritePostRepository.Object);

            // Act
            PostResponse result = await service.GetByIdAsync(postId);
            var message = result.Message;

            // Assert
            message.Should().Be("Post inexistente");
        }

        private Mock<IPostRepository> GetDefaultIPostRepositoryInstance()
        {
            return new Mock<IPostRepository>();
        }

        private Mock<IFavouritePostRepository> GetDefaultIFavouritePostRepositoryInstance()
        {
            return new Mock<IFavouritePostRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}