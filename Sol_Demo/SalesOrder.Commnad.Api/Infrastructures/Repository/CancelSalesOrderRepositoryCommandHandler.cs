using Framework.SqlClient.Helper;
using MediatR;
using SalesOrder.Commnad.Api.Infrastructures.Abstracts;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace SalesOrder.Commnad.Api.Infrastructures.Repository
{
    public class CancelSalesOrderRepository : SalesOrderDTO, IRequest<bool>
    {
    }

    public sealed class CancelSalesOrderRepositoryHandler : SalesOrderRepositoryCommandAbstract, IRequestHandler<CancelSalesOrderRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CancelSalesOrderRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<CancelSalesOrderRepository, bool>.Handle(CancelSalesOrderRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dyanmicParameterTask = base.SetParameterAsync("Cancel-Sales-Order", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dyanmicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        var result = await dbConnection?.ExecuteAsync(sql: "uspSetSalesOrder", param: dynamicParameter, commandType: CommandType.StoredProcedure);
                        return (result >= 1) ? true : false;
                    })
                    ?.ResultAsync<bool>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}