using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class ReviewResponse : BaseResponse<Review>
    {
        public ReviewResponse(Review resource) : base(resource)
        {
        }

        public ReviewResponse(string message) : base(message)
        {
        }
    }
}
