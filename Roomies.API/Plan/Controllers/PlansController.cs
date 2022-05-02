using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Extensions;
using Roomies.API.Plan.Resources;
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
    public class PlansController:ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public PlansController(IPlanService planService, IMapper mapper, IProfileService profileService)
        {
            _planService = planService;
            _mapper = mapper;
            _profileService = profileService;
        }

        [SwaggerOperation(
           Summary = "List all Plans",
           Description = "List of Plans",
           OperationId = "ListAllPlans"
           )]
        [SwaggerResponse(200, "List of Plans", typeof(IEnumerable<PlanResource>))]
        [HttpGet]
        public async Task<IEnumerable<PlanResource>> GetAllAsync()
        {
            var plans = await _planService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Domain.Models.Plan>, IEnumerable<PlanResource>>(plans);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlanResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _planService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var planResource = _mapper.Map<Domain.Models.Plan, PlanResource>(result.Resource);
            return Ok(planResource);
        }

        [HttpPost("plans")]

        public async Task<IActionResult> PostAsync([FromBody] SavePlanResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var plan = _mapper.Map<SavePlanResource, Domain.Models.Plan>(resource);
            var result = await _planService.SaveAsync(plan);

            if (!result.Success)
                return BadRequest(result.Message);

            var planResource= _mapper.Map<Domain.Models.Plan, PlanResource>(result.Resource);

            return Ok(planResource);
        }



    }
}
