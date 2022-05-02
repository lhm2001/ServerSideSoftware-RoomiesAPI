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
    public class LandlordServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public async Task SaveLandlordWhenUserIsYoungerThanRequiredReturnsCantSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            DateTime birthday = new DateTime(2005, 06, 10);


            Domain.Models.Plan plan = new Domain.Models.Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
            {
                UserId = 1,
                Birthday = birthday

            };

            User user = new User
            {
                Id = 1
            };


            List<Profile> profiles = new List<Profile>();
            
            profiles.Add(landlord1);

            mockPlanRepository.Setup(u => u.AddAsync(plan)).Returns(Task.FromResult<Domain.Models.Plan>(plan));
            mockPlanRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Domain.Models.Plan>(plan));

            mockUserRepository.Setup(u => u.AddAsync(user)).Returns(Task.FromResult<User>(user));
            mockUserRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<User>(user));

            mockProfileRepository.Setup(u => u.ListAsync()).Returns(Task.FromResult<IEnumerable<Profile>>(profiles as IEnumerable<Profile>));

            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, 
                mockPostRepository.Object, mockProfileRepository.Object,mockUserRepository.Object);


            // Act


            LandlordResponse result = await service.SaveAsync(landlord1, 1, 1);
            var message = result.Message;


            // Assert

            message.Should().Be("El Landlord debe ser mayor de 18 años");
        }


        [Test]
        public async Task SaveLandlordWhenUserIsOlderThanEighteenReturnsSave()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            DateTime birthday = new DateTime(1990, 06, 10);


            Domain.Models.Plan plan = new Domain.Models.Plan
            {
                Id = 1

            };

            Landlord landlord1 = new Landlord
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

            mockLandlordRepository.Setup(u => u.AddAsync(landlord1)).Returns(Task.FromResult<Landlord>(null));
            mockLandlordRepository.Setup(u => u.FindById(1)).Returns(Task.FromResult<Landlord>(landlord1));

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, 
                mockPostRepository.Object, mockProfileRepository.Object,mockUserRepository.Object);


            // Act


            LandlordResponse result = await service.SaveAsync(landlord1, 1, 1);


            // Assert

            result.Resource.Should().Be(landlord1);
        }

        [Test]
        public async Task GetAllAsyncWhenNoLandlordReturnsEmptyCollection()
        {
            // Arrange

            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            mockLandlordRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Landlord>());

            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object,mockPlanRepository.Object,mockPostRepository.Object,mockProfileRepository.Object,mockUserRepository.Object);

            // Act

            List<Landlord> result = (List<Landlord>)await service.ListAsync();
            var landlordCount = result.Count;

            // Assert

            landlordCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsLandlordNotFoundResponse()
        {
            // Arrange
            var mockLandlordRepository = GetDefaultILandlordRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockProfileRepository = GetDefaultIProfileRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockPlanRepository = GetDefaultIPlanRepositoryInstance();
            var mockUserRepository = GetDefaultUserRepositoryInstance();
            var landlordId = 1;
            Landlord landlord = new Landlord();
            mockLandlordRepository.Setup(r => r.FindById(landlordId)).Returns(Task.FromResult<Landlord>(null));
            var service = new LandlordService(mockLandlordRepository.Object, mockUnitOfWork.Object, mockPlanRepository.Object, mockPostRepository.Object, mockProfileRepository.Object,mockUserRepository.Object);

            // Act
            LandlordResponse result = await service.GetByIdAsync(landlordId);
            var message = result.Message;

            // Assert
            message.Should().Be("Arrendador inexistente");
        }

        private Mock<ILandlordRepository> GetDefaultILandlordRepositoryInstance()
        {
            return new Mock<ILandlordRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }

        private Mock<IProfileRepository> GetDefaultIProfileRepositoryInstance()
        {
            return new Mock<IProfileRepository>();
        }

        private Mock<IPlanRepository> GetDefaultIPlanRepositoryInstance()
        {
            return new Mock<IPlanRepository>();
        }

        private Mock<IUserRepository> GetDefaultUserRepositoryInstance()
        {
            return new Mock<IUserRepository>();
        }

        private Mock<IPostRepository> GetDefaultIPostRepositoryInstance()
        {
            return new Mock<IPostRepository>();
        }
    }
}