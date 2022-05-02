using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class PlanResponse : BaseResponse<Domain.Models.Plan>
    {
        public PlanResponse(Domain.Models.Plan resource) : base(resource)
        {
        }

        public PlanResponse(string message) : base(message)
        {
        }
    }
}
