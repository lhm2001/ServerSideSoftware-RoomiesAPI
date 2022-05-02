using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        Task<IEnumerable<User>> GetAll();
       
        // IEnumerable<User> GetAll();
        Task<UserResponse> Register(RegisterRequest request);

        Task<UserResponse> SaveAsync(User user);
    }
}