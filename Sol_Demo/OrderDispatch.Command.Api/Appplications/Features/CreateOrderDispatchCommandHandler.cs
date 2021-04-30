using AutoMapper;
using MediatR;
using OrderDispatch.Command.Api.Infrastructures.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace OrderDispatch.Command.Api.Appplications.Features
{
    [DataContract]
    public class CreateOrderDispatchCommand : IRequest<bool>
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? OrderIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ShipDate { get; set; }
    }

    public sealed class CreateOrderDispatchCommandHandler : IRequestHandler<CreateOrderDispatchCommand, bool>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public CreateOrderDispatchCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        Task<bool> IRequestHandler<CreateOrderDispatchCommand, bool>.Handle(CreateOrderDispatchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return mediator.Send<bool>(mapper.Map<CreateOrderDispatchRepository>(request));
            }
            catch
            {
                throw;
            }
        }
    }
}