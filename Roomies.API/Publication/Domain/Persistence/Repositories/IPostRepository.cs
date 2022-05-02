using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> ListAsync();
        Task<IEnumerable<Post>> ListByLandlordIdAsync(int landlordId);
        Task AddAsync(Post post);
        Task<Post> FindById(int postId);
        void Update(Post post);
        void Remove(Post post);
        

    }
}
