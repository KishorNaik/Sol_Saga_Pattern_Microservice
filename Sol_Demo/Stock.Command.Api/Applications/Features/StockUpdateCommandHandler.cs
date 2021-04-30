using AutoMapper;
using MediatR;
using Stock.Command.Api.Infrastructures.Repository;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Stock.Command.Api.Applications.Features
{
    [DataContract]
    public class StockUpdateCommand : IRequest<IStockDTO>
    {
        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Quantity { get; set; }
    }

    public sealed class StockUpdateCommandHandler : IRequestHandler<StockUpdateCommand, IStockDTO>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public StockUpdateCommandHandler(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        Task<IStockDTO> IRequestHandler<StockUpdateCommand, IStockDTO>.Handle(StockUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return mediator.Send<IStockDTO>(mapper.Map<StockUpdateRepository>(request));
            }
            catch
            {
                throw;
            }
        }
    }
}