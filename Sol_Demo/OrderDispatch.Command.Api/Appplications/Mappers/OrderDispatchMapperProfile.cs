using AutoMapper;
using OrderDispatch.Command.Api.Appplications.Features;
using OrderDispatch.Command.Api.Infrastructures.Repository;
using OrderDispatch.Shared.DTO;
using OrderDispatch.Shared.Message.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDispatch.Command.Api.Appplications.Mappers
{
    public class OrderDispatchMapperProfile : Profile
    {
        public OrderDispatchMapperProfile()
        {
            OrderDispatchRequestSourceToCreateOrderDispatchCommandDestination();
            CreateOrderDispatchCommandSourceToCreateOrderDispatchRepositoryDestination();
            OrderDispatchRequestSourceToCancelOrderDispatchRepositoryDestination();
        }

        private void OrderDispatchRequestSourceToCreateOrderDispatchCommandDestination()
        {
            base.CreateMap<OrderDispatchRequest, CreateOrderDispatchCommand>();
        }

        private void CreateOrderDispatchCommandSourceToCreateOrderDispatchRepositoryDestination()
        {
            base.CreateMap<CreateOrderDispatchCommand, CreateOrderDispatchRepository>();
        }

        private void OrderDispatchRequestSourceToCancelOrderDispatchRepositoryDestination()
        {
            base.CreateMap<OrderDispatchRequest, CancelOrderDispatchRepository>();
        }
    }
}