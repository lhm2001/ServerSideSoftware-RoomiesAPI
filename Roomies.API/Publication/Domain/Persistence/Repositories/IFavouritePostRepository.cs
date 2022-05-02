using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IFavouritePostRepository
    {
        Task<IEnumerable<FavouritePost>> ListAsync();
        Task<IEnumerable<FavouritePost>> ListByPostIdAsync(int postId);
        Task<IEnumerable<FavouritePost>> ListByLeaseholderIdAsync(int leaseholderId);
        Task<FavouritePost> FindByPostIdAndLeaseholderId(int postId, int leaseholderId);
        Task AddAsync(FavouritePost favouritePost);
        void Remove(FavouritePost favouritePost);
        Task AssignFavouritePostAsync(int postId, int leaseholderId);
        Task UnassignFavouritePostAsync(int postId, int leaseholderId);

    }
}
