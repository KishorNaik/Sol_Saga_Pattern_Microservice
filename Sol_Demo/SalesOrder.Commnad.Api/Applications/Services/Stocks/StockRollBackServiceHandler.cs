using AutoMapper;
using MassTransit;
using MediatR;
using Stock.Shared.Domain;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Services.Stocks
{
    public class StockRollBackService : IRequest
    {
        public IStockDTO Stock { get; set; }
    }

    public class StockRollBackServiceHandler : IRequestHandler<StockRollBackService>
    {
        private readonly IBus bus = null;
        private readonly IMapper mapper = null;

        public StockRollBackServiceHandler(IBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        async Task<Unit> IRequestHandler<StockRollBackService, Unit>.Handle(StockRollBackService request, CancellationToken cancellationToken)
        {
            try
            {
                var endpoint = await bus.GetSendEndpoint(new Uri("queue:stock-rollback-queue"));
                await endpoint.Send<StockRequest>(mapper.Map<StockRequest>(request));

                return new Unit();
            }
            catch
            {
                throw;
            }
        }
    }
}