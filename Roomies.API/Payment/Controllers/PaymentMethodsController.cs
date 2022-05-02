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
    public class PaymentMethodsController:ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IPaymentMethodService paymentMethodService, IMapper mapper)
        {
            _paymentMethodService = paymentMethodService;
            _mapper = mapper;
        }

        [SwaggerOperation(
           Summary = "List all PaymentMethods",
           Description = "List of PaymentMethods",
           OperationId = "ListAllPaymentMethods"
           )]
        [SwaggerResponse(200, "List of PaymentMethods", typeof(IEnumerable<PaymentMethodResource>))]

        [HttpGet]
        public async Task<IEnumerable<PaymentMethodResource>> GetAllAsync()
        {
            var paymentMethods = await _paymentMethodService.ListAsync();
            var resources = _mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<PaymentMethodResource>>(paymentMethods);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentMethodResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _paymentMethodService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);
            return Ok(paymentMethodResource);
        }

        [HttpPost]

        public async Task<IActionResult> PostAsync([FromBody] SavePaymentMethodResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var paymentMethod = _mapper.Map<SavePaymentMethodResource, PaymentMethod>(resource);
            var result = await _paymentMethodService.SaveAsync(paymentMethod);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);

            return Ok(paymentMethodResource);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _paymentMethodService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var paymentMethodResource = _mapper.Map<PaymentMethod, PaymentMethodResource>(result.Resource);

            return Ok(paymentMethodResource);

        }
    }
}
