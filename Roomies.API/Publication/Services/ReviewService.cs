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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ILeaseholderRepository _leaseholderRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IReviewRepository reviewRepository, IUnitOfWork unitOfWork, ILeaseholderRepository leaseholderRepository = null, IPostRepository postRepository = null)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _leaseholderRepository = leaseholderRepository;
            _postRepository = postRepository;
        }


        public async Task<ReviewResponse> DeleteAsync(int id)
        {
            var existingReview = await _reviewRepository.FindById(id);

            if (existingReview == null)
                return new ReviewResponse("Reseña inexistente");

            try
            {
                _reviewRepository.Remove(existingReview);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(existingReview);
            }
            catch (Exception ex)
            {
                return new ReviewResponse($"Un error ocurrió al buscar la reseña: {ex.Message}");
            }
        }

        public async Task<ReviewResponse> GetByIdAsync(int reviewId)
        {
            var existingReview = await _reviewRepository.FindById(reviewId);

            if (existingReview == null)
                return new ReviewResponse("Review inexistente");

            return new ReviewResponse(existingReview);
        }

        public async Task<IEnumerable<Review>> ListAsync()
        {
            return await _reviewRepository.ListAsync();
        }


        public async Task<IEnumerable<Review>> ListByLeaseholderIdAsync(int leaseholderId)
        {
            return await _reviewRepository.ListByLeaseholderId(leaseholderId);
        }

        public async Task<IEnumerable<Review>> ListByPostIdAsync(int postId)
        {
            return await _reviewRepository.ListByPostId(postId);
        }

        public async Task<ReviewResponse> SaveAsync(Review review,int leaseholderId,int postId)
        {

            var existingLeaseholder = await _leaseholderRepository.FindById(leaseholderId);

            if (existingLeaseholder == null)
                return new ReviewResponse("Arrendatario inexistente");

            var existingPost= await _postRepository.FindById(postId);

            if (existingPost == null)
                return new ReviewResponse("Post inexistente");

            try
            {
                review.LeaseholderId = leaseholderId;
                review.PostId = postId;

                await _reviewRepository.AddAsync(review);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(review);
            }
            catch (Exception ex)
            {
                return new ReviewResponse($"Un error ocurrió al guardar la reseña: {ex.Message}");
            }
        }

        public async Task<ReviewResponse> UpdateAsync(int id, Review review)
        {
            var existingReview = await _reviewRepository.FindById(id);

            if (existingReview == null)
                return new ReviewResponse("Review inexistente");

            existingReview.Content = review.Content;

            try
            {
                _reviewRepository.Update(existingReview);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(existingReview);
            }
            catch (Exception ex)
            {
                return new ReviewResponse($"Un error ocurrió al actualizar la reseña: {ex.Message}");
            }
        }
    }
}
