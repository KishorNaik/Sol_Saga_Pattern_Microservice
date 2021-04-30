using Framework.SqlClient.Helper;
using MediatR;
using SalesOrder.Commnad.Api.Infrastructures.Abstracts;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace SalesOrder.Commnad.Api.Infrastructures.Repository
{
    public class CreateSalesOrderRepository : SalesOrderDTO, IRequest<ISalesOrderDTO>
    {
    }

    public sealed class CreateSalesOrderRepositoryHandler : SalesOrderRepositoryCommandAbstract, IRequestHandler<CreateSalesOrderRepository, ISalesOrderDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CreateSalesOrderRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<ISalesOrderDTO> IRequestHandler<CreateSalesOrderRepository, ISalesOrderDTO>.Handle(CreateSalesOrderRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Create-Sales-Order", request);

                var result =
                      sqlClientDbProvider
                      ?.DapperBuilder
                      ?.OpenConnection(sqlClientDbProvider.GetConnection())
                      ?.Parameter(async () => await dynamicParameterTask)
                      ?.Command(async (dbConnection, dynamicParameters) =>
                      {
                          try
                          {
                              var salesOrder =
                                    await
                                    dbConnection
                                    ?.QueryFirstOrDefaultAsync<SalesOrderDTO>(sql: "uspSetSalesOrder", param: dynamicParameters, commandType: CommandType.StoredProcedure);

                              return salesOrder;
                          }
                          catch
                          {
                              throw;
                          }
                      })
                      ?.ResultAsync<ISalesOrderDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}