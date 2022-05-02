using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Domain.Services.Communications;
using Roomies.API.Extensions;
using Roomies.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        private IUserService _userService;
        private IProfileService _profileService;
        private ILeaseholderService _leaseholderService;
        private ILandlordService _landlordService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, IProfileService profileService, ILeaseholderService leaseholderService, ILandlordService landlordService)
        {
            _userService = userService;
            _mapper = mapper;
            _profileService = profileService;
            _leaseholderService = leaseholderService;
            _landlordService = landlordService;
        }

        [SwaggerOperation(
            Summary ="List of all Users",
            Description="List of Users",
            OperationId ="ListAllUsers"
            )]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<UserResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthenticationResponse>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<AuthenticationResponse>> GetAll()
        {
            var users = await _userService.GetAll();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<AuthenticationResponse>>(users);

            return resources;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
                return BadRequest(new { message = "Usuario o  contraseña incorrecto" });

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            UserResponse response =  await _userService.Register(request);

            if (!response.Success)
                return BadRequest(response.Message);

            
            var userResource = _mapper.Map<User, AuthenticationResponse>(response.Resource);

            
            return Ok(userResource);
        }

        [HttpGet("{userId}/profiles")]
        [ProducesResponseType(typeof(ProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int userId)
        {
            var result = await _profileService.GetByUserIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
            return Ok(profileResource);
        }


        [HttpPost("{userId}/plans/{planId}/profiles")]

        public async Task<IActionResult> PostAsync([FromBody] SaveProfileResource resource, int userId,int planId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var profile = _mapper.Map<SaveProfileResource, Domain.Models.Profile>(resource);
            var result = await _profileService.SaveAsync(profile, planId,userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);

            return Ok(profileResource);
        }


        [HttpPost("{userId}/plans/{planId}/leaseholders")]
        public async Task<IActionResult> PostAsync([FromBody] SaveLeaseholderResource resource, int userId, int planId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var leaseholder = _mapper.Map<SaveLeaseholderResource, Leaseholder>(resource);
            var result = await _leaseholderService.SaveAsync(leaseholder, planId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var leaseholderResource = _mapper.Map<Leaseholder, LeaseholderResource>(result.Resource);

            return Ok(leaseholderResource);
        }

        [HttpPost("{userId}/plans/{planId}/landlords")]
        public async Task<IActionResult> PostAsync([FromBody] SaveLandlordResource resource, int userId, int planId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var landlord = _mapper.Map<SaveLandlordResource, Landlord>(resource);
            var result = await _landlordService.SaveAsync(landlord, planId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var landlordResource = _mapper.Map<Landlord, LandlordResource>(result.Resource);

            return Ok(landlordResource);
        }
    }
}
