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
    }
}
