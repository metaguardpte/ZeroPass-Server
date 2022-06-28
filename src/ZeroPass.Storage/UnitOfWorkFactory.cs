﻿using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace ZeroPass.Storage
{
    internal class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        readonly ConnectionOption Options;

        public UnitOfWorkFactory(ConnectionOption options) => Options = options;

        public async Task<IUnitOfWork> CreateRead()
        {
            var conn = await CreateAndOpenConnection(Options.ReadonlyMysqlConnectionString);
            return new UnitOfWork(conn);
        }

        public Task<IUnitOfWork> CreateWrite() => ConnectMaster();

        async Task<IUnitOfWork> ConnectMaster()
        {
            var conn = await CreateAndOpenConnection(Options.MasterMysqlConnectionString);
            return new UnitOfWork(conn);
        }

        async Task<MySqlConnection> CreateAndOpenConnection(string connectionString)
        {
            var conn = new MySqlConnection(connectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}