using Dapper;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Stock.Command.Api.Infrastructures.Abstracts
{
    public abstract class StockRepositoryCommandAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(String command, IStockDTO stockDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    var dynamicParameter = new DynamicParameters();

                    dynamicParameter.Add("@Command", command, DbType.String, ParameterDirection.Input);
                    dynamicParameter.Add("@StockIdentity", stockDTO.StockIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameter.Add("@ProductName", stockDTO.ProductName, DbType.String, ParameterDirection.Input);
                    dynamicParameter.Add("@Quantity", stockDTO.Quantity, DbType.String, ParameterDirection.Input);

                    return dynamicParameter;
                });
            }
            catch
            {
                throw;
            }
        }
    }
}