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
    [Route("/api/profiles/{profileId}/paymentMethods")]
    public class ProfilePaymentMethodsController:ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IProfilePaymentMethodService _profilePaymentMethodService;
        private readonly IMapper _mapper;

        public ProfilePaymentMethodsController(IPaymentMethodService paymentMethodService , IProfilePaymentMethodService userPaymentMethodService, IMapper mapper)
        {
            _paymentMethodService = paymentMethodService;
            _profilePaymentMethodService = userPaymentMethodService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodResource>> GetAllByProfileTdAsync(int profileId)
        {
            var paymentMethods = await _paymentMethodService.ListByProfileIdAsync(profileId);
            var resources = _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodResource>>(paymentMethods);

            return resources;
        }

        [HttpPost("{paymentMethodId}")]
        public async Task<IActionResult> AssignProfilePaymentMethod(int profileId, int paymentMethodId)
        {
            var result = await _profilePaymentMethodService.AssignProfilePaymentMethodAsync(profileId, paymentMethodId);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource.PaymentMethod);

            return Ok(paymentMethodResource);
        }
    }
}
