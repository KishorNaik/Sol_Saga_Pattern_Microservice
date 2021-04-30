using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Commnad.Api.Applications.Features;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/sales-order")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public SalesOrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSalesOrderAsync([FromBody] CreateSalesOrderCommand createSalesOrderCommand)
            => base.Ok(await mediator.Send<bool>(createSalesOrderCommand));

        [NonAction]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSalesOrderAsync([FromBody] CancelSalesOrderCommand cancelSalesOrderCommand)
            => base.Ok(await mediator.Send<bool>(cancelSalesOrderCommand));
    }
}