using AutoMapper;
using MassTransit;
using MediatR;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using Stock.Shared.Message.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Services.Stocks
{
    public class StockUpdateService : IRequest<IStockDTO>
    {
        public IStockDTO Stock { get; set; }
    }

    public class StockUpdateServiceHandler : IRequestHandler<StockUpdateService, IStockDTO>
    {
        private readonly IBus bus = null;
        private readonly IMapper mapper = null;

        public StockUpdateServiceHandler(IBus bus, IMapper mapper)
        {
            this.bus = bus;
            this.mapper = mapper;
        }

        async Task<IStockDTO> IRequestHandler<StockUpdateService, IStockDTO>.Handle(StockUpdateService request, CancellationToken cancellationToken)
        {
            try
            {
                var client = bus.CreateRequestClient<StockRequest>(new Uri("queue:stock-update-queue"));

                //var response = await client.GetResponse<IStockDTO>(mapper.Map<StockDTO>(request));
                var response = await client.GetResponse<StockResponse>(new StockRequest()
                {
                    ProductName = request.Stock.ProductName,
                    Quantity = request.Stock.Quantity
                });

                var stockDTO = new StockDTO()
                {
                    ProductName = response.Message.ProductName,
                    Quantity = response.Message.Quantity,
                    Status = response.Message.Status,
                    StockIdentity = response.Message.StockIdentity
                };

                return stockDTO;
            }
            catch
            {
                throw;
            }
        }
    }
}