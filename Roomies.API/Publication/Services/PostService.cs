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
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ILandlordRepository _landlordRepository;
        private readonly ILeaseholderRepository _leaseholderRepository;
        private readonly IFavouritePostRepository _favouritePostRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, IFavouritePostRepository favouritePostRepository, ILandlordRepository landlordRepository = null, ILeaseholderRepository leaseholderRepository = null, IReviewRepository reviewRepository = null)
        {
            _postRepository = postRepository;
            _favouritePostRepository = favouritePostRepository;
            _unitOfWork = unitOfWork;
            _landlordRepository = landlordRepository;
            _leaseholderRepository = leaseholderRepository;
            _reviewRepository = reviewRepository;
        }


        public async Task<PostResponse> DeleteAsync(int id)
        {
            var existingPost = await _postRepository.FindById(id);

            if (existingPost == null)
                return new PostResponse("Post inexistente");

            try
            {
                if (existingPost.Reviews!=null) { 
                 existingPost.Reviews.ForEach(delegate (Review review)
                  {
                      _reviewRepository.Remove(review);
                 });
                }

                _postRepository.Remove(existingPost);
                await _unitOfWork.CompleteAsync();

                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"Un error ocurrió al buscar el post: {ex.Message}");
            }
        }

        public async Task<PostResponse> GetByIdAsync(int postId)
        {
            var existingPost= await _postRepository.FindById(postId);

            if (existingPost == null)
                return new PostResponse("Post inexistente");

            return new PostResponse(existingPost);
        }

        public async Task<IEnumerable<Post>> ListAsync()
        {
            return await _postRepository.ListAsync();
        }

        public async Task<IEnumerable<Post>> ListByLandlordIdAsync(int landlordId)
        {
            return await _postRepository.ListByLandlordIdAsync(landlordId);
        }

        public async Task<IEnumerable<Post>> ListByLeaseholderIdAsync(int leaseholderId)
        {
            var favouritePosts = await _favouritePostRepository.ListByLeaseholderIdAsync(leaseholderId);
            var post = favouritePosts.Select(pt => pt.Post).ToList();
            return post;
        }


        public async Task<PostResponse> SaveAsync(Post post,int landlordId)
        {
            var existingLandlord = await _landlordRepository.FindById(landlordId);

            if (existingLandlord== null)
                return new PostResponse("Arrendador inexistente");


            try
            {
                post.LandlordId = landlordId;

                await _postRepository.AddAsync(post);
                await _unitOfWork.CompleteAsync();

                return new PostResponse(post);
            }
            catch (Exception ex)
            {
                return new PostResponse($"Un error ocurrió al guardar el post: {ex.Message}");
            }
        }

        public async Task<PostResponse> UpdateAsync(int id, Post post)
        {
            var existingPost = await _postRepository.FindById(id);

            if (existingPost == null)
                return new PostResponse("Post inexistente");

            existingPost.Title = post.Title;
            existingPost.Address = post.Address;
            existingPost.BathroomQuantity = post.BathroomQuantity;
            existingPost.Price = post.Price;
            existingPost.Province = post.Province;
            existingPost.District = post.District;
            existingPost.Department = post.Department;
            existingPost.RoomQuantity = post.RoomQuantity;
            existingPost.Description = post.Description;

            try
            {
                _postRepository.Update(existingPost);
                await _unitOfWork.CompleteAsync();

                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"Un error ocurrió al actualizar el post: {ex.Message}");
            }
        }
    }
}
