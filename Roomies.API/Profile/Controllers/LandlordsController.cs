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
    public class LandlordsController : ControllerBase
    {
        private readonly ILandlordService _landlordService;
        private readonly IMapper _mapper;

        public LandlordsController(ILandlordService landlordService, IMapper mapper)
        {
            _landlordService = landlordService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all Landlords",
           Description = "List of Landlords",
           OperationId = "ListAllLandlords"
           )]
        [SwaggerResponse(200, "List of Landlords", typeof(IEnumerable<LandlordResource>))]
        [HttpGet]
        public async Task<IEnumerable<LandlordResource>> GetAllAsync()
        {
            var landlords = await _landlordService.ListAsync();//ListByCategoryIdAsync(categoryId);
            var resources = _mapper.Map<IEnumerable<Landlord>, IEnumerable<LandlordResource>>(landlords);

            return resources;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LandlordResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _landlordService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var landlordResource = _mapper.Map<Landlord, LandlordResource>(result.Resource);
            return Ok(landlordResource);
        }
        //---------------------------

        //-------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveLandlordResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var landlord = _mapper.Map<SaveLandlordResource, Landlord>(resource);
            var result = await _landlordService.UpdateAsync(id, landlord);

            if (!result.Success)
                return BadRequest(result.Message);

            var landlordResource = _mapper.Map<Landlord, LandlordResource>(result.Resource);

            return Ok(landlordResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _landlordService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var landlordResource = _mapper.Map<Landlord, LandlordResource>(result.Resource);

            return Ok(landlordResource);

        }

    }
}
