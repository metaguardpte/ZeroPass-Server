﻿using Dapper;
using System.Data;
using System.Threading.Tasks;
using ZeroPass.Storage.Entities;

namespace ZeroPass.Storage
{
    internal class DomainRepository : IDomainRepository
    {
        readonly IDbConnection Connection;

        public DomainRepository(IUnitOfWork unitOfWork) => Connection = unitOfWork.Connection;

        public Task<int> Insert(DomainEntity entity)
        {
            var sql = "insert into t_domain" +
                "(domain_type, domain_name, company, create_time)" +
                "values " +
                "(@DomainType, @DomainName, @Company, @CreateTime);" +
                "select last_insert_id();";
            return Connection.ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task InsertDomainInfo(DomainInfoEntity entity)
        {
            var sql = "insert into t_domain_info" +
                "(domain_id, contact_phone, contact_person, number_of_employees, country, timezone, logo)" +
                "values " +
                "(@DomainId, @ContactPhone, @ContactPerson, @NumberOfEmployees, @Country, @Timezone, @Logo);" +
                "select last_insert_id();";
            await Connection.ExecuteAsync(sql, entity);
        }
    }
}
