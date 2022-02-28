using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using RestApi.Data;
using RestApi.Models;

namespace RestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private MsqlConfiguration _connectionString;

        public UserRepository(MsqlConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var db = dbConnection();

            var sql = @"SELECT id, username FROM user";

            return await db.QueryAsync<User>(sql, new { });
        }

        public async Task<User> GetUser(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM user WHERE id = @Id";

            return await db.QueryFirstAsync<User>(sql, new { Id = id });
        }

        public async Task<User> Create(User user)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO user(username) VALUES(@username);" + "SELECT LAST_INSERT_ID()";

            user.Id = await db.QuerySingleAsync<int>(sql, new { user.Username });

            return user;
        }

        public async Task<bool> Update(User user, int id)
        {
            var db = dbConnection();

            var sql = @"UPDATE user SET username = @Username WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { user.Username, id });

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM user WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;

        }
    }
}
