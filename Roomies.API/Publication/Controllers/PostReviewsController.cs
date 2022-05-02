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
    [Route("/api/posts/{postId}/reviews")]
    public class PostReviewsController
    {
        private IReviewService _reviewService;
        private readonly IMapper _mapper;

        public PostReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewResource>> GetAllByPostIdAsync(int postId)
        {
            var reviews = await _reviewService.ListByPostIdAsync(postId);
            var resources = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewResource>>(reviews);

            return resources;
        }
        //se va a POST
    }
}