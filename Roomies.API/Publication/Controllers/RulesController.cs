using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Extensions;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Domain.Services;
using Roomies.API.Publication.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Publication.Controllers
{
    [Route("/api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        private readonly IMapper _mapper;

        public RulesController(IRuleService ruleService,IMapper mapper)
        {
            _mapper = mapper;
            _ruleService = ruleService;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RuleResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<RuleResource>> GetAllAsync()
        {
            var rules = await _ruleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Rule>, IEnumerable<RuleResource>>(rules);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RuleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _ruleService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Resource);
            return Ok(ruleResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRuleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var rule = _mapper.Map<SaveRuleResource, Rule>(resource);
            var result = await _ruleService.UpdateAsync(id, rule);

            if (!result.Success)
                return BadRequest(result.Message);

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Resource);

            return Ok(ruleResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ruleService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Resource);

            return Ok(ruleResource);
        }
    }
}
