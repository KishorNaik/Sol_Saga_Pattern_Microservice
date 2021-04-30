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
    public class StockUpdateRepository : StockDTO, IRequest<IStockDTO>
    {
    }

    public sealed class StockUpdateRepositoryHandler : StockRepositoryCommandAbstract, IRequestHandler<StockUpdateRepository, IStockDTO>
    {
        private readonly ISqlClientDbProvider sqlClientDbProvider = null;

        public StockUpdateRepositoryHandler(ISqlClientDbProvider sqlClientDbProvider)
        {
            this.sqlClientDbProvider = sqlClientDbProvider;
        }

        Task<IStockDTO> IRequestHandler<StockUpdateRepository, IStockDTO>.Handle(StockUpdateRepository request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicParameterTask = base.SetParameterAsync("Stock-Update", request);

                var result =
                        sqlClientDbProvider
                        ?.DapperBuilder
                        ?.OpenConnection(sqlClientDbProvider.GetConnection())
                        ?.Parameter(async () => await dynamicParameterTask)
                        ?.Command(async (dbConnection, dynamicParameter) =>
                        {
                            StockDTO stockDTO = null;

                            try
                            {
                                var sqlGrid =
                                    await
                                    dbConnection
                                    ?.QueryMultipleAsync(sql: "uspSetStock", param: dynamicParameter, commandType: CommandType.StoredProcedure);

                                var message = sqlGrid.ReadFirst();

                                if (message.Message == "Quantity available")
                                {
                                    stockDTO = sqlGrid.ReadFirstOrDefault<StockDTO>();
                                    stockDTO.Quantity = request?.Quantity;
                                    stockDTO.Status = message.Message;
                                }
                                else
                                {
                                    stockDTO = new StockDTO()
                                    {
                                        Status = message.Message
                                    };
                                }

                                return stockDTO;
                            }
                            catch
                            {
                                throw;
                            }
                        })

                        ?.ResultAsync<IStockDTO>();

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}