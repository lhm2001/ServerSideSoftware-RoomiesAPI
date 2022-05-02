using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BCryptNet = BCrypt.Net;
using AutoMapper;
using Roomies.API.Domain.Services.Communications;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Roomies.API.Settings;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Persistence.Repositories;

namespace Roomies.API.Services
{
    public class UserService:IUserService
    {

        private readonly AppSettings _appSettings;

        private readonly IUserRepository _userRepository;
       
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;


        public UserService(IOptions<AppSettings> appSettings, IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponse> GetByUsernameAsync(string username)
        {
            var existingPlan = await _userRepository.FindByUsername(username);

            if (existingPlan == null)
                return new UserResponse("Plan inexistente");

            return new UserResponse(existingPlan);
        }
        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            IEnumerable<User> users = await _userRepository.ListAsync();

            var user = users.ToList().SingleOrDefault(x => x.Username == request.Username);

            if (user == null || !BCryptNet.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new ApplicationException("Username or password is incorrect");
            }

            if (user == null) return null;

            var response = _mapper.Map<User, AuthenticationResponse>(user);
            response.Token = GenerateJwtToken(user);

            return response;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.ListAsync();
        }

        public async Task< UserResponse> Register(RegisterRequest request)
        {
            IEnumerable<User> users = await _userRepository.ListAsync();

            if (users.Any(x => x.Username == request.Username))
                return new UserResponse($"El usuario {request.Username} ya existe");
            else
            {
                var user = _mapper.Map<RegisterRequest, User>(request);

                user.PasswordHash = BCryptNet.BCrypt.HashPassword(request.Password);

                return await SaveAsync(user);
            }
            
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();



                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"Un error ocurrió al guardar el usuario: {ex.Message}");
            }
        }

        private string GenerateJwtToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
