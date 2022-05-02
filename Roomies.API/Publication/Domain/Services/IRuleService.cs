using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Domain.Services
{
    public interface IRuleService
    {
        Task<IEnumerable<Rule>> ListAsync();
        Task<IEnumerable<Rule>> ListByPostId(int postId);
        Task<RuleResponse> GetByIdAsync(int id);
        Task<RuleResponse> SaveAsync(int postId, Rule rule);
        Task<RuleResponse> UpdateAsync(int id, Rule ruleRequest);
        Task<RuleResponse> DeleteAsync(int id);

    }
}
