using AutoMapper;
using MassTransit;
using MediatR;
using OrderDispatch.Command.Api.Appplications.Features;
using OrderDispatch.Command.Api.Infrastructures.Repository;
using OrderDispatch.Shared.DTO;
using OrderDispatch.Shared.Message.Requests;
using OrderDispatch.Shared.Message.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDispatch.Command.Api.Appplications.Messages
{
    public class CreateOrderDispatchConsumeMessage : IConsumer<OrderDispatchRequest>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateOrderDispatchConsumeMessage(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task IConsumer<OrderDispatchRequest>.Consume(ConsumeContext<OrderDispatchRequest> context)
        {
            try
            {
                var result = await mediator.Send<bool>(mapper.Map<CreateOrderDispatchCommand>(context.Message));

                try
                {
                    //throw new Exception("Create Order Failed");
                    await context.RespondAsync<OrderDispatchResponse>(new OrderDispatchResponse()
                    {
                        OrderIdentity = context.Message.OrderIdentity,
                        ShipDate = context.Message.ShipDate,
                        Status = result.ToString()
                    });
                }
                catch
                {
                    _ = await mediator.Send<bool>(mapper.Map<CancelOrderDispatchRepository>(context.Message));
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