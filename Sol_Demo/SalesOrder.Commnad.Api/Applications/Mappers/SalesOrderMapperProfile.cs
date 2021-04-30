using AutoMapper;
using OrderDispatch.Shared.DTO;
using SalesOrder.Commnad.Api.Applications.Features;
using SalesOrder.Commnad.Api.Applications.Services.OrderDispatch;
using SalesOrder.Commnad.Api.Applications.Services.Stocks;
using SalesOrder.Commnad.Api.Infrastructures.Repository;
using Stock.Shared.DTO;
using Stock.Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Mappers
{
    public class SalesOrderMapperProfile : Profile
    {
        public SalesOrderMapperProfile()
        {
            this.CreateSalesOrderCommandSourceToCreateSalesOrderCommandRepositoryDestination();
            this.CancelSalesOrderCommandSourceToCancelSalesOrderCommandRepositoryDestination();
            this.StockDTOSourceToStockUpdateServiceDestination();
            this.StockRollBackServiceSourceToStockRequestDestination();
            this.CreateOrderDispatchServiceSourceToOrderDispatchDTODestination();
        }

        private void CreateSalesOrderCommandSourceToCreateSalesOrderCommandRepositoryDestination()
        {
            base.CreateMap<CreateSalesOrderCommand, CreateSalesOrderRepository>();
        }

        private void CancelSalesOrderCommandSourceToCancelSalesOrderCommandRepositoryDestination()
        {
            base.CreateMap<CancelSalesOrderCommand, CancelSalesOrderRepository>();
        }

        private void CreateOrderDispatchServiceSourceToOrderDispatchDTODestination()
        {
            base.CreateMap<CreateOrderDispatchService, OrderDispatchDTO>()
                .ForMember((dest) => dest.OrderIdentity, (opt) => opt.MapFrom((src) => src.SalesOrder.SalesOrderIdentity))
                .ForMember((dest) => dest.ShipDate, (opt) => opt.MapFrom((src) => src.OrderDispatch.ShipDate));
        }

        private void StockDTOSourceToStockUpdateServiceDestination()
        {
            base.CreateMap<StockDTO, StockUpdateService>()
                .ForPath((dest) => dest.Stock.ProductName, (opt) => opt.MapFrom((src) => src.ProductName))
                .ForPath((dest) => dest.Stock.Quantity, (opt) => opt.MapFrom((src) => src.Quantity))
                .ForPath((dest) => dest.Stock.Status, (opt) => opt.MapFrom((src) => src.Status))
                .ForPath((dest) => dest.Stock.StockIdentity, (opt) => opt.MapFrom((src) => src.StockIdentity));
        }

        private void StockRollBackServiceSourceToStockRequestDestination()
        {
            base.CreateMap<StockRollBackService, StockRequest>()
               .ForMember((dest) => dest.ProductName, (opt) => opt.MapFrom((src) => src.Stock.ProductName))
               .ForMember((dest) => dest.Quantity, (opt) => opt.MapFrom((src) => src.Stock.Quantity))
               .ForMember((dest) => dest.Status, (opt) => opt.MapFrom((src) => src.Stock.Status))
               .ForMember((dest) => dest.StockIdentity, (opt) => opt.MapFrom((src) => src.Stock.StockIdentity));
        }
    }
}