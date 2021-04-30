using Dapper;
using OrderDispatch.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace OrderDispatch.Command.Api.Infrastructures.Abstracts
{
    public abstract class OrderDispatchRepositoryAbstract
    {
        protected Task<DynamicParameters> SetParameterAsync(string command, IOrderDispatchDTO orderDispatchDTO)
        {
            try
            {
                return Task.Run(() =>
                {
                    var dynamicParameter = new DynamicParameters();

                    dynamicParameter.Add("@Command", command, DbType.String, ParameterDirection.Input);
                    dynamicParameter.Add("@ShippingIdentity", orderDispatchDTO?.ShippingIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameter.Add("@OrderIdentity", orderDispatchDTO?.OrderIdentity, DbType.Guid, ParameterDirection.Input);
                    dynamicParameter.Add("@ShipDate", orderDispatchDTO?.ShipDate, DbType.Date, ParameterDirection.Input);

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