using Framework.SqlClient.Helper;
using MediatR;
using OrderDispatch.Command.Api.Infrastructures.Abstracts;
using OrderDispatch.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace OrderDispatch.Command.Api.Infrastructures.Repository
{
    public class CreateOrderDispatchRepository : OrderDispatchDTO, IRequest<bool>
    {
    }

    public sealed class CreateOrderDispatchRepositoryHandler : OrderDispatchRepositoryAbstract, IRequestHandler<CreateOrderDispatchRepository, bool>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public CreateOrderDispatchRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<bool> IRequestHandler<CreateOrderDispatchRepository, bool>.Handle(CreateOrderDispatchRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Create-Order-Dispatch", request);

                var result =
                    sqlClientDbProvider
                    ?.DapperBuilder
                    ?.OpenConnection(sqlClientDbProvider?.GetConnection())
                    ?.Parameter(async () => await dynamicParameterTask)
                    ?.Command(async (dbConnection, dynamicParameter) =>
                    {
                        try
                        {
                            var result = await dbConnection?.ExecuteAsync(sql: "uspSetOrderDispatch", param: dynamicParameter, commandType: CommandType.StoredProcedure);
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