using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<IEnumerable<Review>> ListByPostId(int postId);
        Task<IEnumerable<Review>> ListByLeaseholderId(int leaseholderId);
        Task<Review> FindById(int id);

        Task AddAsync(Review review);
        void Remove(Review review);
        void Update(Review review);

    }
}
