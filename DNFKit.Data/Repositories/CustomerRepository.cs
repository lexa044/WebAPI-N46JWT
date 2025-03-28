﻿using System.Data;

using Dapper;

using DNFKit.Core;
using DNFKit.Core.Models;
using DNFKit.Core.Repositories;

namespace DNFKit.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDalSession _session;

        public CustomerRepository(IDalSession session)
        {
            _session = session;
        }

        public Customer FindBy(Customer filter)
        {
            var connection = _session.GetReadOnlyConnection();
            var p = new DynamicParameters();
            p.Add("inIdType", filter.IdType);
            p.Add("inIdNumber", filter.IdNumber);
            return connection.QueryFirstOrDefault<Customer>(sql: "dbo.API_GetCustomerByIdNumber", param: p, commandType: CommandType.StoredProcedure);
        }

        public Customer UpdateBy(Customer filter)
        {
            var connection = _session.GetReadOnlyConnection();
            var p = new DynamicParameters();
            p.Add("inIdType", filter.IdType);
            p.Add("inIdNumber", filter.IdNumber);
            return connection.QueryFirstOrDefault<Customer>(sql: "dbo.API_UpdateCustomerByIdNumber", param: p, commandType: CommandType.StoredProcedure);
        }
    }
}
