using AutoMapper;
using MassTransit;
using MediatR;
using Stock.Command.Api.Applications.Features;
using Stock.Command.Api.Infrastructures.Repository;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using Stock.Shared.Message.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Command.Api.Applications.Message
{
    public class StockUpdateConsumeMessage : IConsumer<StockRequest>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public StockUpdateConsumeMessage(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<StockRequest>.Consume(ConsumeContext<StockRequest> context)
        {
            try
            {
                var result = await mediator.Send<IStockDTO>(mapper.Map<StockUpdateCommand>(context.Message));
                try
                {
                    //throw new Exception("Stock Update Failed");
                    await context.RespondAsync<StockResponse>(new StockResponse()
                    {
                        ProductName = result.ProductName,
                        Quantity = result.Quantity,
                        StockIdentity = result.StockIdentity,
                        Status = result.Status
                    });
                }
                catch
                {
                    _ = await mediator.Send<bool>(mapper.Map<StockRollBackRepository>(result));
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}