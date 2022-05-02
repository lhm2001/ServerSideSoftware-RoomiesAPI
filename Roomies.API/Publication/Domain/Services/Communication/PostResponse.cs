using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class PostResponse : BaseResponse<Post>
    {
        public PostResponse(Post resource) : base(resource)
        {
        }

        public PostResponse(string message) : base(message)
        {
        }
    }
}
