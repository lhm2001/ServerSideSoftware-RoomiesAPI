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
    [Route("/api/leaseholders/{leaseholderId}/posts")]
    public class FavouritePostsController:ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IFavouritePostService _favouritePostService;
        private readonly IMapper _mapper;

        public FavouritePostsController(IPostService postService, IFavouritePostService favouritePostService, IMapper mapper)
        {
            _postService = postService;
            _favouritePostService = favouritePostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PostResource>> GetAllByLeaseholderIdAsync(int leaseholderId)
        {
            var posts = await _postService.ListByLeaseholderIdAsync(leaseholderId);
            var resources = _mapper.Map<IEnumerable<Post>, IEnumerable<PostResource>>(posts);

            return resources;
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> AssignFavouritePost(int leaseholderId, int postId)
        {
            var result = await _favouritePostService.AssignFavouritePostAsync(postId, leaseholderId);

            if (!result.Success)
                return BadRequest(result.Message);

            //var postResource = _mapper.Map<Post,PostResource>(result.Resource.);

            return Ok("Se ha agregado a favorito el Post");
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> UnAssignFavouritePost(int leaseholderId, int postId)
        {
            var result = await _favouritePostService.UnassignFavouritePostAsync(postId, leaseholderId);

            if (!result.Success)
                return BadRequest(result.Message);

            //var postResource = _mapper.Map<Post,PostResource>(result.Resource.);

            return Ok("Se ha quitado de favorito el Post");
        }
    }
}
