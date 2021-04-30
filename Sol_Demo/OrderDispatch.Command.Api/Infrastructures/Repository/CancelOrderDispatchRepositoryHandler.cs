using Framework.SqlClient.Helper;
using MediatR;
using OrderDispatch.Command.Api.Infrastructures.Abstracts;
using OrderDispatch.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace OrderDispatch.Command.Api.Infrastructures.Repository
{
    public class CancelOrderDispatchRepository : OrderDispatchDTO, IRequest<bool>
    {
    }

    public sealed class CancelOrderDispatchRepositoryHandler : OrderDispatchRepositoryAbstract, IRequestHandler<CancelOrderDispatchRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CancelOrderDispatchRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<CancelOrderDispatchRepository, bool>.Handle(CancelOrderDispatchRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Cancel-Order-Dispatch", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dyanmicParameter) =>
                    {
                        try
                        {
                            var result = await dbConnection?.ExecuteAsync(sql: "uspSetOrderDispatch", param: dyanmicParameter, commandType: CommandType.StoredProcedure);
                            return (result > 1) ? true : false;
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