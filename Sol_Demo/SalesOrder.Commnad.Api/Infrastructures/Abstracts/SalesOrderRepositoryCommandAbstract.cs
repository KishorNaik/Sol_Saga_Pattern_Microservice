using Dapper;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SalesOrder.Commnad.Api.Infrastructures.Abstracts
{
    public abstract class SalesOrderRepositoryCommandAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(String command, ISalesOrderDTO salesOrderDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    var dynamicParameters = new DynamicParameters();

                    dynamicParameters.Add("@Command", command, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@SalesOrderIdentity", salesOrderDTO?.SalesOrderIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameters.Add("@SalesOrderNumber", salesOrderDTO?.SalesOrderNumber, DbType.Guid, ParameterDirection.Input);
                    dynamicParameters.Add("@OrderDate", salesOrderDTO?.OrderDate, DbType.Date, ParameterDirection.Input);
                    dynamicParameters.Add("@ProductName", salesOrderDTO?.ProductName, DbType.String, ParameterDirection.Input);
                    dynamicParameters.Add("@OrderQty", salesOrderDTO?.OrderQty, DbType.Int32, ParameterDirection.Input);
                    dynamicParameters.Add("@UnitPrice", salesOrderDTO?.UnitPrice, DbType.Decimal, ParameterDirection.Input);

                    return dynamicParameters;
                });
            }
            catch
            {
                throw;
            }
        }
    }
}