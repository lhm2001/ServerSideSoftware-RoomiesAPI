using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
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
        public class ProfilesController : ControllerBase
        {
            private readonly IProfileService _profileService;
            private readonly IMapper _mapper;

            public ProfilesController(IProfileService profileService, IMapper mapper)
            {
                _profileService = profileService;
                _mapper = mapper;
            }

            [SwaggerOperation(
               Summary = "List all Users",
               Description = "List of Users",
               OperationId = "ListAllUsers"
               )]
            [SwaggerResponse(200, "List of Users", typeof(IEnumerable<ProfileResource>))]
            [HttpGet]
            public async Task<IEnumerable<ProfileResource>> GetAllAsync()
            {
                var profiles = await _profileService.ListAsync();
                var resources = _mapper.Map<IEnumerable<Domain.Models.Profile>, IEnumerable<ProfileResource>>(profiles);

                return resources;
            }
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(ProfileResource), 200)]
            [ProducesResponseType(typeof(BadRequestResult), 404)]
            public async Task<IActionResult> GetAsync(int id)
            {
                var result = await _profileService.GetByIdAsync(id);

                if (!result.Success)
                    return BadRequest(result.Message);

                var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
                return Ok(profileResource);
            }
            //-------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProfileResource resource)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessages());

                var profile = _mapper.Map<SaveProfileResource, Domain.Models.Profile>(resource);
                var result = await _profileService.UpdateAsync(id, profile);

                if (!result.Success)
                    return BadRequest(result.Message);

                var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);

                return Ok(profileResource);

            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
            {
                var result = await _profileService.DeleteAsync(id);

                if (!result.Success)
                    return BadRequest(result.Message);

                var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);

                return Ok(profileResource);

            }

        }
    
}
