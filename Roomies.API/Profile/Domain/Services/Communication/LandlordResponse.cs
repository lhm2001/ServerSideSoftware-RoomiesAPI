using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class LandlordResponse : BaseResponse<Landlord>
    {
        public LandlordResponse(Landlord resource) : base(resource)
        {
        }

        public LandlordResponse(string message) : base(message)
        {
        }
    }
}
