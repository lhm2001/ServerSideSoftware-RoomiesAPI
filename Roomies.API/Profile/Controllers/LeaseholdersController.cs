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
    public class LeaseholdersController : ControllerBase
    {
        private readonly ILeaseholderService _leaseholderService;
        private readonly IFavouritePostService _favouritePostService;
        private readonly IMapper _mapper;

        public LeaseholdersController(ILeaseholderService leaseholderService, IMapper mapper, IFavouritePostService favouritePostService)
        {
            _leaseholderService = leaseholderService;
            _mapper = mapper;
            _favouritePostService = favouritePostService;
        }

        [SwaggerOperation(
           Summary = "List all Leaseholders",
           Description = "List of Leaseholders",
           OperationId = "ListAllLeaseholders"
           )]
        [SwaggerResponse(200, "List of Leaseholders", typeof(IEnumerable<LeaseholderResource>))]
        [HttpGet]
        public async Task<IEnumerable<LeaseholderResource>> GetAllAsync()
        {
            var leaseholders = await _leaseholderService.ListAsync();//ListByCategoryIdAsync(categoryId);
            var resources = _mapper.Map<IEnumerable<Leaseholder>, IEnumerable<LeaseholderResource>>(leaseholders);

            return resources;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LeaseholderResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _leaseholderService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var leaseholderResource = _mapper.Map<Leaseholder, LeaseholderResource>(result.Resource);
            return Ok(leaseholderResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveLeaseholderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var leaseholder = _mapper.Map<SaveLeaseholderResource, Leaseholder>(resource);
            var result = await _leaseholderService.UpdateAsync(id, leaseholder);

            if (!result.Success)
                return BadRequest(result.Message);

            var leaseholderResource = _mapper.Map<Leaseholder, LeaseholderResource>(result.Resource);

            return Ok(leaseholderResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _leaseholderService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var leaseholderResource = _mapper.Map<Leaseholder, LeaseholderResource>(result.Resource);

            return Ok(leaseholderResource);

        }

        [HttpPost("{leaseholderId}/posts/{postId}")]
        public async Task<IActionResult> AssignFavouritePost(int leaseholderId, int postId)
        {
            var result = await _favouritePostService.AssignFavouritePostAsync(postId, leaseholderId);

            if (!result.Success)
                return BadRequest(result.Message);


            return Ok("Se ha agregado a favorito el Post");
        }
        [HttpDelete("{leaseholderId}/posts/{postId}")]
        public async Task<IActionResult> UnAssignFavouritePost(int leaseholderId, int postId)
        {
            var result = await _favouritePostService.UnassignFavouritePostAsync(postId, leaseholderId);

            if (!result.Success)
                return BadRequest(result.Message);


            return Ok("Se ha quitado de favorito el Post");
        }

    }
}
