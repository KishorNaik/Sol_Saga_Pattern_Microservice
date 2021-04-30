using Framework.SqlClient.Helper;
using MediatR;
using Stock.Command.Api.Infrastructures.Abstracts;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Stock.Command.Api.Infrastructures.Repository
{
    public class StockRollBackRepository : StockDTO, IRequest<bool>
    {
    }

    public sealed class StockRollBackRepositoryHandler : StockRepositoryCommandAbstract, IRequestHandler<StockRollBackRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public StockRollBackRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<StockRollBackRepository, bool>.Handle(StockRollBackRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Stock-RollBack", request);

                var result =
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await dynamicParameterTask)
                        ?.Command(async (dbConnection, dynamicParameter) =>
                        {
                            try
                            {
                                var result = await dbConnection?.ExecuteAsync(sql: "uspSetStock", param: dynamicParameter, commandType: CommandType.StoredProcedure);
                                return (result >= 1) ? true : false;
                            }
                            catch
                            {
                                throw;
                            }
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