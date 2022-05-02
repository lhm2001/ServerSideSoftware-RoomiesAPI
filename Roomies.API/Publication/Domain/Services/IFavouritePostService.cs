using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IFavouritePostService
    {
        Task<IEnumerable<FavouritePost>> ListAsync();
        Task<IEnumerable<FavouritePost>> ListByPostIdAsync(int postId);
        Task<IEnumerable<FavouritePost>> ListByLeaseholderIdAsync(int leaseholderId);
        Task<FavouritePostResponse> AssignFavouritePostAsync(int postId, int leaseholderId);
        Task<FavouritePostResponse> UnassignFavouritePostAsync(int postId, int leaseholderId);
    }
}
