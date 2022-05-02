using NUnit.Framework;
using Moq;
using FluentAssertions;
using Roomies.API.Domain.Repositories;
using Roomies.API.Services;
using Roomies.API.Domain.Services.Communications;
using System.Threading.Tasks;
using System.Collections.Generic;
using Roomies.API.Domain.Models;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Services;
using Roomies.API.Publication.Domain.Persistence.Repositories;
using Roomies.API.Publication.Domain.Services.Communication;

namespace Roomies.API.Test
{
    public class RuleServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllAsyncWhenNoRuleReturnsEmptyCollection()
        {
            // Arrange

            var mockRuleRepository = GetDefaultIRuleRepositoryInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockRuleRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Rule>());

            var service = new RuleService(mockRuleRepository.Object, mockUnitOfWork.Object,mockPostRepository.Object);

            // Act

            List<Rule> result = (List<Rule>)await service.ListAsync();
            var ruleCount = result.Count;

            // Assert

            ruleCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsRuleNotFoundResponse()
        {
            // Arrange
            var mockRuleRepository = GetDefaultIRuleRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockPostRepository = GetDefaultIPostRepositoryInstance();

            var ruleId = 1;
            Rule rule = new Rule();
            mockRuleRepository.Setup(r => r.FindById(ruleId)).Returns(Task.FromResult<Rule>(null));

            var service = new RuleService(mockRuleRepository.Object, mockUnitOfWork.Object, mockPostRepository.Object);

            // Act
            RuleResponse result = await service.GetByIdAsync(ruleId);
            var message = result.Message;

            // Assert
            message.Should().Be("Rule inexistente");
        }

        private Mock<IRuleRepository> GetDefaultIRuleRepositoryInstance()
        {
            return new Mock<IRuleRepository>();
        }

        private Mock<IPostRepository> GetDefaultIPostRepositoryInstance()
        {
            return new Mock<IPostRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}