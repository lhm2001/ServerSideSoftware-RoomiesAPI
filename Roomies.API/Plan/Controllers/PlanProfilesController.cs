using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Resources;

namespace Roomies.API.Controllers
{
    [Route("/api/plans/{planId}/profiles")]
    public class PlanProfilesController
    {
        private IProfileService _profileService;
        private readonly IMapper _mapper;

        public PlanProfilesController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProfileResource>> GetAllByPlanIdAsync(int planId)
        {
            var profiles = await _profileService.ListByPlanIdAsync(planId);
            var resources = _mapper.Map<IEnumerable<Domain.Models.Profile>, IEnumerable<ProfileResource>>(profiles);

            return resources;
        }
    }
}
