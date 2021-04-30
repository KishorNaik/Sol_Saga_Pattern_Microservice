using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Command.Api.Applications.Features;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Command.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator mediator = null;

        public StockController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // Testing

        [NonAction]
        [HttpPost("stock-update")]
        public async Task<IActionResult> StockUpdateAsync([FromBody] StockUpdateCommand stockUpdateCommand)
            => base.Ok(await mediator.Send<IStockDTO>(stockUpdateCommand));
    }
}