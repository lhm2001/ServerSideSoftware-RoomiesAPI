using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services.Communications
{
    public class FavouritePostResponse : BaseResponse<FavouritePost>
    {
        public FavouritePostResponse(FavouritePost resource) : base(resource)
        {
        }

        public FavouritePostResponse(string message) : base(message)
        {
        }
    }
}
