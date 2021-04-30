using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDispatch.Command.Api.Appplications.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDispatch.Command.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/order-dispatch")]
    [ApiController]
    public class OrderDispatchController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public OrderDispatchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [NonAction]
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderDispatch([FromBody] CreateOrderDispatchCommand createOrderDispatchCommand)
            => base.Ok(await mediator.Send<bool>(createOrderDispatchCommand));
    }
}