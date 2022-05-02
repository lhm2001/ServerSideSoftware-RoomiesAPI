using Roomies.API.Domain.Models;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Services
{
    public class FavouritePostService : IFavouritePostService
    {
        private readonly IFavouritePostRepository _favouritePostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FavouritePostService(IFavouritePostRepository favouritePostRepository, IUnitOfWork unitOfWork)
        {
            _favouritePostRepository = favouritePostRepository;
            _unitOfWork = unitOfWork;
        }
 

        public async Task<FavouritePostResponse> AssignFavouritePostAsync(int postId, int leaseholderId)
        {
            try
            {
                await _favouritePostRepository.AssignFavouritePostAsync(postId, leaseholderId);
                await _unitOfWork.CompleteAsync();
                FavouritePost favouritePost = await _favouritePostRepository.FindByPostIdAndLeaseholderId(postId, leaseholderId);
                return new FavouritePostResponse(favouritePost);

            }
            catch (Exception ex)
            {
                return new FavouritePostResponse($"Un error ocurrió al asignar Post a Usuario: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FavouritePost>> ListAsync()
        {
            return await _favouritePostRepository.ListAsync();
        }

        public async Task<IEnumerable<FavouritePost>> ListByLeaseholderIdAsync(int leaseholderId)
        {
            return await _favouritePostRepository.ListByLeaseholderIdAsync(leaseholderId);
        }

        public async Task<IEnumerable<FavouritePost>> ListByPostIdAsync(int postId)
        {
            return await _favouritePostRepository.ListByPostIdAsync(postId);
        }

        public async Task<FavouritePostResponse> UnassignFavouritePostAsync(int postId, int leaseholderId)
        {

            try
            {
                FavouritePost favouritePost = await _favouritePostRepository.FindByPostIdAndLeaseholderId(postId, leaseholderId);

                _favouritePostRepository.Remove(favouritePost);
                await _unitOfWork.CompleteAsync();

                return new FavouritePostResponse(favouritePost);

            }
            catch (Exception ex)
            {
                return new FavouritePostResponse($"Un error ocurrió al desasignar Post a Usuario: {ex.Message}");
            }
        }
    }
}
