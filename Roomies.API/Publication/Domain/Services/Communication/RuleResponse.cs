using Roomies.API.Publication.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Domain.Services.Communication
{
    public class RuleResponse : BaseResponse<Rule>
    {
        public RuleResponse(Rule resource) : base(resource)
        {
        }

        public RuleResponse(string message) : base(message)
        {
        }
    }
}
