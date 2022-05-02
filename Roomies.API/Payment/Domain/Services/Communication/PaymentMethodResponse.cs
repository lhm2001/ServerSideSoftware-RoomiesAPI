using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class PaymentMethodResponse : BaseResponse<PaymentMethod>
    {
        public PaymentMethodResponse(PaymentMethod resource) : base(resource)
        {
        }

        public PaymentMethodResponse(string message) : base(message)
        {
        }
    }
}
