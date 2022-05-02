using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> ListAsync();
        Task<IEnumerable<Post>> ListByLandlordIdAsync(int landlordId);
        Task<IEnumerable<Post>> ListByLeaseholderIdAsync(int leaseholderId);
        Task<PostResponse> GetByIdAsync(int postId);
        Task<PostResponse> SaveAsync(Post post,int landlordId);
        Task<PostResponse> UpdateAsync(int id, Post post);
        Task<PostResponse> DeleteAsync(int id);
    }
}
