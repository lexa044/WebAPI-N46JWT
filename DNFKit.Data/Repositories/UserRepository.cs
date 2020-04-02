using Dapper;

using DNFKit.Core;
using DNFKit.Core.Models;
using DNFKit.Core.Repositories;
using System.Data;

namespace DNFKit.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDalSession _session;

        public UserRepository(IDalSession session)
        {
            _session = session;
        }

        public User FindById(int id)
        {
            var connection = _session.GetReadOnlyConnection();
            var p = new DynamicParameters();
            p.Add("inId", id, DbType.Int32);
            return connection.QueryFirstOrDefault<User>(sql: "dbo.API_GetUserById", param: p, transaction: null, commandType: CommandType.StoredProcedure);
        }

        public User FindByUsername(string username)
        {
            var connection = _session.GetReadOnlyConnection();
            var p = new DynamicParameters();
            p.Add("inUsername", username, DbType.AnsiString);
            return connection.QueryFirstOrDefault<User>(sql: "dbo.API_GetUserByUsername", param: p, transaction: null, commandType: CommandType.StoredProcedure);
        }

        public User Update(User model)
        {
            var uom = _session.GetUnitOfWork();
            var connection = uom.GetConnection();
            var p = new DynamicParameters();
            p.Add("inId", model.Id, DbType.Int32);
            p.Add("inUsername", model.Username, DbType.AnsiString);
            p.Add("inPasswordHash", model.PasswordHash, DbType.AnsiString);
            p.Add("inPasswordSeed", model.PasswordSeed, DbType.AnsiString);
            p.Add("inToken", model.Token, DbType.AnsiString);
            p.Add("inExpiresIn", model.ExpiresIn, DbType.DateTime);

            connection.Execute(sql: "dbo.API_UpdateUser", param: p, transaction: uom.GetTransaction(), commandType: CommandType.StoredProcedure);

            return model;
        }
    }
}
