﻿using Dapper;
using System.Data;
using System.Threading.Tasks;
using ZeroPass.Storage.Entities;

namespace ZeroPass.Storage
{
    internal class UserRepository : IUserRepository
    {
        readonly IDbConnection Connection;
        public UserRepository(IUnitOfWork unitOfWork) => Connection = unitOfWork.Connection;

        public async Task<UserEntity> GetByEmail(string email)
        {
            var sql = 
                $@"select id, email, user_type, user_name
                from t_user 
                where email = @Email";
            return await Connection.QueryFirstOrDefaultAsync<UserEntity>(sql, new { Email = email });
        }

        public Task<int> Insert(UserEntity entity)
        {
            var sql = "insert into t_user" +
               "(user_type, user_name, email)" +
               "values " +
               "(@UserType, @UserName, @Email);" +
               "select last_insert_id();";
            return Connection.ExecuteScalarAsync<int>(sql, entity);
        }
    }
}
