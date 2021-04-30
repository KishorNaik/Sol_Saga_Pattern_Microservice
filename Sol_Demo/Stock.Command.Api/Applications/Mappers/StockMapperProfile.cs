using AutoMapper;
using Stock.Command.Api.Applications.Features;
using Stock.Command.Api.Infrastructures.Repository;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Command.Api.Applications.Mappers
{
    public class StockMapperProfile : Profile
    {
        public StockMapperProfile()
        {
            StockUpdateCommandSourceToStockUpdateRepositoryCommandDestination();
            StockRequestSourceToStockRollBackRepositoryDestination();
            StockRequestSourceToStockUpdateCommandDestination();
            StockDTOSourceToStockRollBackRepositoryDestination();
        }

        private void StockUpdateCommandSourceToStockUpdateRepositoryCommandDestination()
        {
            base.CreateMap<StockUpdateCommand, StockUpdateRepository>();
        }

        private void StockRequestSourceToStockRollBackRepositoryDestination()
        {
            base.CreateMap<StockRequest, StockRollBackRepository>();
        }

        private void StockRequestSourceToStockUpdateCommandDestination()
        {
            base.CreateMap<StockRequest, StockUpdateCommand>();
        }

        private void StockDTOSourceToStockRollBackRepositoryDestination()
        {
            base.CreateMap<StockDTO, StockRollBackRepository>();
        }
    }
}