using BComm.PM.Models.Auth;
using BComm.PM.Repositories.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly string _connectionString;

        public UserQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DbConfig:connStr").Value;
        }

        public async Task<User> GetByUserNamePassword(string userName, string password)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"UserName\"=@username and \"Password\"=@password",
                    TableNameConstants.UsersTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<User>(query, new { username = userName, password = password });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<User>> GetByUsername(string userName)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"UserName\"=@username",
                    TableNameConstants.UsersTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return await conn.QueryAsync<User>(query, new { username = userName });
            }
        }

        public async Task<User> GetById(string userId)
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0} where \"HashId\"=@id",
                    TableNameConstants.UsersTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                var result = await conn.QueryAsync<User>(query, new { @id = userId });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = new StringBuilder()
                    .AppendFormat("select * from {0}",
                    TableNameConstants.UsersTable)
                    .ToString();

            using (IDbConnection conn = new NpgsqlConnection(_connectionString))
            {
                return await conn.QueryAsync<User>(query);
            }
        }
    }
}
