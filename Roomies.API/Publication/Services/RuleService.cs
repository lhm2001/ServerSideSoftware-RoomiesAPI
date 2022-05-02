using Roomies.API.Domain.Repositories;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Domain.Persistence.Repositories;
using Roomies.API.Publication.Domain.Services;
using Roomies.API.Publication.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostRepository _postRepository;

        public RuleService(IRuleRepository ruleRepository, IUnitOfWork unitOfWork, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _ruleRepository = ruleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RuleResponse> DeleteAsync(int id)
        {
            var existingRule = await _ruleRepository.FindById(id);

            if (existingRule == null)
                return new RuleResponse("Rule inexistente");

            try
            {
                _ruleRepository.Remove(existingRule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(existingRule);
            }
            catch(Exception ex)
            {
                return new RuleResponse($"Un error ocurrió al buscar la regla: {ex.Message}");
            }

        }

        public async Task<RuleResponse> GetByIdAsync(int id)
        {
            var existingRule = await _ruleRepository.FindById(id);

            if (existingRule == null)
                return new RuleResponse("Rule inexistente");

            return new RuleResponse(existingRule);
        }

        public async Task<IEnumerable<Rule>> ListAsync()
        {
            return await _ruleRepository.ListAsync();
        }

        public async Task<IEnumerable<Rule>> ListByPostId(int postId)
        {
            return await _ruleRepository.ListByPostId(postId);
        }

        public async Task<RuleResponse> SaveAsync(int postId, Rule rule)
        {
            var existingPost = await _postRepository.FindById(postId);

            if (existingPost == null)
                return new RuleResponse("Post inexistente");

            try
            {
                rule.PostId = postId;

                await _ruleRepository.AddAsync(rule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(rule);
            }
            catch(Exception ex)
            {
                return new RuleResponse($"Un error ocurrió al guardar la regla: {ex.Message}");
            }
        }

        public async Task<RuleResponse> UpdateAsync(int id, Rule ruleRequest)
        {
            var existingRule = await _ruleRepository.FindById(id);

            if (existingRule== null)
                return new RuleResponse("Post inexistente");

            existingRule.Title = ruleRequest.Title;
            existingRule.Description = ruleRequest.Description;

            try
            {
                _ruleRepository.Update(existingRule);
                await _unitOfWork.CompleteAsync();

                return new RuleResponse(existingRule);
            }
            catch(Exception ex)
            {
                return new RuleResponse($"Un error ocurrió el actualizar la regla: {ex.Message}");
            }
        }
    }
}
