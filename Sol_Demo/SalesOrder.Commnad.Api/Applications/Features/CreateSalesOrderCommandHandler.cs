using AutoMapper;
using MediatR;
using SalesOrder.Commnad.Api.Applications.Saga.Orchestration;
using SalesOrder.Commnad.Api.Infrastructures.Repository;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Features
{
    [DataContract]
    public class CreateSalesOrderCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public DateTime? OrderDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? OrderQty { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double? UnitPrice { get; set; }
    }

    public sealed class CreateSalesOrderCommandHandler : IRequestHandler<CreateSalesOrderCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateSalesOrderCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<CreateSalesOrderCommand, bool>.Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Create a Order.
                var salesOrderData = await mediator.Send<ISalesOrderDTO>(mapper.Map<CreateSalesOrderRepository>(request));

                // Start Order Saga Orchestration
                var sagaContext = await mediator.Send<bool>(new SalesOrderOrchestration()
                {
                    SalesOrder = salesOrderData
                });

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}