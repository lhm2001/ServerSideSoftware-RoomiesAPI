using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<IEnumerable<Review>> ListByLeaseholderIdAsync(int leaseholderId);
        Task<IEnumerable<Review>> ListByPostIdAsync(int postId);
        Task<ReviewResponse> GetByIdAsync(int reviewId);
        Task<ReviewResponse> SaveAsync(Review review,int leaseholderId, int postId);
        Task<ReviewResponse> UpdateAsync(int id, Review review);
        Task<ReviewResponse> DeleteAsync(int id);
    }
}
