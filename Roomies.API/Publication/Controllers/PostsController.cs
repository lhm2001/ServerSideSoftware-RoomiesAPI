using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services;
using Roomies.API.Extensions;
using Roomies.API.Publication.Domain.Models;
using Roomies.API.Publication.Domain.Services;
using Roomies.API.Publication.Resources;
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

    public class PostsController:ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IRuleService _ruleService;
        private IReviewService _reviewService;

        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper, IReviewService reviewService, IRuleService ruleService)
        {
            _ruleService = ruleService;
            _postService = postService;
            _mapper = mapper;
            _reviewService = reviewService;
        }

        [SwaggerOperation(
           Summary = "List all Posts",
           Description = "List of Posts",
           OperationId = "ListAllPosts"
           )]
        [SwaggerResponse(200, "List of Posts", typeof(IEnumerable<PostResource>))]
        [HttpGet]
        public async Task<IEnumerable<PostResource>> GetAllAsync()
        {
            var posts = await _postService.ListAsync();//ListByCategoryIdAsync(categoryId);
            var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PostResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _postService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var postResource = _mapper.Map<Post, PostResource>(result.Resource);
            return Ok(postResource);
        }

        [HttpGet("{postId}/rules")]
        public async Task<IEnumerable<RuleResource>> GetAllRulesAsync(int postId)
        {
            var rules= await _ruleService.ListByPostId(postId);
            var resources = _mapper.Map<IEnumerable<Rule>, IEnumerable<RuleResource>>(rules);

            return resources;
        }

        [HttpPost("posts/{postId}/rules")]
        public async Task<IActionResult> PostAsync([FromBody] SaveRuleResource resource, int postId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var rules = _mapper.Map<SaveRuleResource, Rule>(resource);
            var result = await _ruleService.SaveAsync(postId, rules);

            if (!result.Success)
                return BadRequest(result.Message);

            var ruleResource = _mapper.Map<Rule, RuleResource>(result.Resource);

            return Ok(ruleResource);

        }

        [HttpPost("landlords/{landlordId}/posts")]

        public async Task<IActionResult> PostAsync([FromBody] SavePostResource resource, int landlordId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var post = _mapper.Map<SavePostResource, Post>(resource);
            var result = await _postService.SaveAsync(post, landlordId);

            if (!result.Success)
                return BadRequest(result.Message);

            var postResource = _mapper.Map<Post, PostResource>(result.Resource);

            return Ok(postResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePostResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var post = _mapper.Map<SavePostResource, Post>(resource);
            var result = await _postService.UpdateAsync(id, post);

            if (!result.Success)
                return BadRequest(result.Message);

            var postResource = _mapper.Map<Post, PostResource>(result.Resource);

            return Ok(postResource);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _postService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var postResource = _mapper.Map<Post, PostResource>(result.Resource);

            return Ok(postResource);

        }
    }
}
