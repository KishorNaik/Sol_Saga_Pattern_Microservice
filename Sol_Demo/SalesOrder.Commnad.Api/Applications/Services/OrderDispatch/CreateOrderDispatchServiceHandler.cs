using AutoMapper;
using MassTransit;
using MediatR;
using OrderDispatch.Shared.DTO;
using OrderDispatch.Shared.Message.Requests;
using OrderDispatch.Shared.Message.Responses;
using SalesOrder.Commnad.Api.Applications.Rules;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Services.OrderDispatch
{
    public class CreateOrderDispatchService : IRequest<IOrderDispatchDTO>
    {
        public ISalesOrderDTO SalesOrder { get; set; }

        public IOrderDispatchDTO OrderDispatch { get; set; }
    }

    public sealed class CreateOrderDispatchServiceHandler : IRequestHandler<CreateOrderDispatchService, IOrderDispatchDTO>
    {
        private readonly IBus bus = null;
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateOrderDispatchServiceHandler(IBus bus, IMediator mediator, IMapper mapper)
        {
            this.bus = bus;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<IOrderDispatchDTO> IRequestHandler<CreateOrderDispatchService, IOrderDispatchDTO>.Handle(CreateOrderDispatchService request, CancellationToken cancellationToken)
        {
            try
            {
                var client = bus.CreateRequestClient<OrderDispatchRequest>(new Uri("queue:order-dispatch-queue"));

                var response = await client.GetResponse<OrderDispatchResponse>(new OrderDispatchRequest()
                {
                    OrderIdentity = request.SalesOrder.SalesOrderIdentity,
                    ShipDate = request.OrderDispatch.ShipDate
                });

                //var response = await client.GetResponse<OrderDispatchDTO>(mapper.Map<OrderDispatchDTO>(request));

                var orderDispatchResponse = response.Message;

                return new OrderDispatchDTO()
                {
                    OrderIdentity = orderDispatchResponse.OrderIdentity,
                    ShipDate = orderDispatchResponse.ShipDate,
                    Status = orderDispatchResponse.Status
                };
            }
            catch
            {
                throw;
            }
        }
    }
}