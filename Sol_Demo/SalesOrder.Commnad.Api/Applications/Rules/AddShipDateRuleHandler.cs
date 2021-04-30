using MediatR;
using OrderDispatch.Shared.DTO;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Rules
{
    public class AddShipDateRule : INotification
    {
        public ISalesOrderDTO SalesOrder { get; set; }

        public IOrderDispatchDTO OrderDispatch { get; set; }
    }

    public class AddShipDateRuleHandler : INotificationHandler<AddShipDateRule>
    {
        Task INotificationHandler<AddShipDateRule>.Handle(AddShipDateRule notification, CancellationToken cancellationToken)
        {
            notification.OrderDispatch.ShipDate = notification.SalesOrder.OrderDate.Value.AddDays(2);

            return Task.CompletedTask;
        }
    }
}