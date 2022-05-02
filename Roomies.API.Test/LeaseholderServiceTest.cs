using NUnit.Framework;
using Moq;
using FluentAssertions;
using Roomies.API.Domain.Repositories;
using Roomies.API.Services;
using Roomies.API.Domain.Services.Communications;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Persistence.Repositories;
using System;

namespace Roomies.API.Test
{
    public class LeaseholderServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SaveLeaseholderWhenUserIsYoungerThanRequiredReturnsCantSave()
        {
            // Arrange

            var mockLeaseholderRepository = GetDefaultILeaseholderRepositoryInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockFavouritePostRepository = GetDefaultFavouritePostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            DateTime birthday = new DateTime(2005, 06, 10);


            Domain.Models.Plan plan = new Domain.Models.Plan
            {
                Id = 1

            };

            User user = new User
            {
                Id = 1
            };

            Leaseholder leaseholder1 = new Leaseholder
            {
                UserId = 1,
                Birthday = birthday

            };


            List<Profile> profiles = new List<Profile>();
            //:(
            profiles.Add(leaseholder1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Domain.Models.Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Domain.Models.Plan>(plan));

            mockUserRepository.Setup(u => u.AddAsync(user)).Returns(Task.FromResult<User>(user));
            mockUserRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<User>(user));

            mockProfileRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<Profile>>(profiles as IEnumerable<Profile>));

            mockLeaseholderRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Leaseholder>(leaseholder1));

            var service = new LeaseholderService(mockLeaseholderRepository.Object, mockFavouritePostRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockProfileRepository.Object, mockUserRepository.Object);


            // Act


            LeaseholderResponse result = await service.SaveAsync(leaseholder1, 1, 1);
            var message = result.Message;


            // Assert

            message.Should().Be("El Leaseholder debe ser mayor de 18 años");
        }

        [Test]
        public async Task SaveLeaseholderWhenUserIsOlderThanEighteenReturnsSave()
        {
            // Arrange

            var mockLeaseholderRepository = GetDefaultILeaseholderRepositoryInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockFavouritePostRepository = GetDefaultFavouritePostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            DateTime birthday = new DateTime(1990, 06, 10);


            Domain.Models.Plan plan = new Domain.Models.Plan
            {
                Id = 1

            };

            Leaseholder leaseholder1 = new Leaseholder
            {
                UserId = 1,
                Birthday = birthday

            };

            User user = new User
            {
                Id = 1
            };

            List<Profile> profiles = new List<Profile>();

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Domain.Models.Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Domain.Models.Plan>(plan));

            mockUserRepository.Setup(u => u.AddAsync(user)).Returns(Task.FromResult<User>(user));
            mockUserRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<User>(user));

            mockLeaseholderRepository.Setup(u => u.AddAsync(leaseholder1)).Returns(Task.FromResult<Leaseholder>(null));
            mockLeaseholderRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Leaseholder>(leaseholder1));

            var service = new LeaseholderService(mockLeaseholderRepository.Object, mockFavouritePostRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockProfileRepository.Object, mockUserRepository.Object);


            // Act


            LeaseholderResponse result = await service.SaveAsync(leaseholder1, 1, 1);


            // Assert

            result.Resource.Should().Be(leaseholder1);
        }


        [Test]
        public async Task GetAllAsyncWhenNoLeaseholderReturnsEmptyCollection()
        {
            // Arrange

            var mockLeaseholderRepository = GetDefaultILeaseholderRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockFavouritePostRepository = GetDefaultFavouritePostRepositoryInstance();

            mockLeaseholderRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Leaseholder>());

            var service = new LeaseholderService(mockLeaseholderRepository.Object, mockFavouritePostRepository.Object, mockUnitOfWork.Object);

            // Act

            List<Leaseholder> result = (List<Leaseholder>)await service.ListAsync();
            var leaseholderCount = result.Count;

            // Assert

            leaseholderCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsLeaseholderNotFoundResponse()
        {
            // Arrange
            var mockLeaseholderRepository = GetDefaultILeaseholderRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockFavouritePostRepository = GetDefaultFavouritePostRepositoryInstance();

            var leaseholderId = 1;
            Leaseholder leaseholder = new Leaseholder();
            mockLeaseholderRepository.Setup(r => r.FindById(leaseholderId)).Returns(Task.FromResult<Leaseholder>(null));
            var service = new LeaseholderService(mockLeaseholderRepository.Object, mockFavouritePostRepository.Object, mockUnitOfWork.Object);

            // Act
            LeaseholderResponse result = await service.GetByIdAsync(leaseholderId);
            var message = result.Message;

            // Assert
            message.Should().Be("Arrendatario inexistente");
        }

        private Mock<ILeaseholderRepository> GetDefaultILeaseholderRepositoryInstance()
        {
            return new Mock<ILeaseholderRepository>();
        }

        private Mock<IProfileRepository> GetDefaultIProfileRepositoryInstance()
        {
            return new Mock<IProfileRepository>();
        }

        private Mock<IPlanRepository> GetDefaultIPlanRepositoryInstance()
        {
            return new Mock<IPlanRepository>();
        }

        private Mock<IFavouritePostRepository> GetDefaultFavouritePostRepositoryInstance()
        {
            return new Mock<IFavouritePostRepository>();
        }

        private Mock<IUserRepository> GetDefaultUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }


    }
}