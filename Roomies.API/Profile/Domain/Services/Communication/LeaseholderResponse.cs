using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class LeaseholderResponse : BaseResponse<Leaseholder>
    {
        public LeaseholderResponse(Leaseholder resource) : base(resource)
        {
        }

        public LeaseholderResponse(string message) : base(message)
        {
        }
    }
}
