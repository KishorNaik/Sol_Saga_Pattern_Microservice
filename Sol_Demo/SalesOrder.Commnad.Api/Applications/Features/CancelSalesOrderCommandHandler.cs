using AutoMapper;
using MediatR;
using SalesOrder.Commnad.Api.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Features
{
    [DataContract]
    public class CancelSalesOrderCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? SalesOrderIdentity { get; set; }
    }

    public sealed class CancelSalesOrderCommandHandler : IRequestHandler<CancelSalesOrderCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CancelSalesOrderCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        Task<bool> IRequestHandler<CancelSalesOrderCommand, bool>.Handle(CancelSalesOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return mediator.Send<bool>(mapper.Map<CancelSalesOrderRepository>(request));
            }
            catch
            {
                throw;
            }
        }
    }
}