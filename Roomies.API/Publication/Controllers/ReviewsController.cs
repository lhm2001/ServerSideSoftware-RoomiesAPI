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
    public class ReviewsController:ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all Reviews",
           Description = "List of Reviews",
           OperationId = "ListAllReviews"
           )]
        [SwaggerResponse(200, "List of Reviews", typeof(IEnumerable<ReviewResource>))]
        [HttpGet]
        public async Task<IEnumerable<ReviewResource>> GetAllAsync()
        {
            var reviews = await _reviewService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewResource>>(reviews);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _reviewService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);
            return Ok(reviewResource);
        }

        [HttpPost("leaseholders/{leaseholderId}/posts/{postId}/reviews")]
        public async Task<IActionResult> PostAsync([FromBody] SaveReviewResource resource, int leaseholderId,int postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var review = _mapper.Map<SaveReviewResource, Review>(resource);
            var result = await _reviewService.SaveAsync(review, leaseholderId,postId);

            if (!result.Success)
                return BadRequest(result.Message);

            var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

            return Ok(reviewResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var review = _mapper.Map<SaveReviewResource, Review>(resource);
            var result = await _reviewService.UpdateAsync(id, review);

            if (!result.Success)
                return BadRequest(result.Message);

            var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

            return Ok(reviewResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reviewService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var reviewResource = _mapper.Map<Review, ReviewResource>(result.Resource);

            return Ok(reviewResource);

        }
    }
}
