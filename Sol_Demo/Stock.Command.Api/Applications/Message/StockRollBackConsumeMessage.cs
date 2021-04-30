using AutoMapper;
using MassTransit;
using MediatR;
using Stock.Command.Api.Applications.Features;
using Stock.Command.Api.Infrastructures.Repository;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Command.Api.Applications.Message
{
    public class StockRollBackConsumeMessage : IConsumer<StockRequest>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public StockRollBackConsumeMessage(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<StockRequest>.Consume(ConsumeContext<StockRequest> context)
        {
            _ = await mediator.Send<bool>(mapper.Map<StockRollBackRepository>(context.Message));
        }
    }
}