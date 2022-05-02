using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class ProfilePaymentMethodResponse : BaseResponse<ProfilePaymentMethod>
    {
        public ProfilePaymentMethodResponse(ProfilePaymentMethod resource) : base(resource)
        {
        }

        public ProfilePaymentMethodResponse(string message) : base(message)
        {
        }
    }
}
