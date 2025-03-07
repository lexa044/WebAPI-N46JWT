using System.Data;

using Dapper;

using DNFKit.Core;
using DNFKit.Core.Models;
using DNFKit.Core.Repositories;

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
            p.Add("inId", id);
            return connection.QueryFirstOrDefault<User>(sql: "dbo.API_GetUserById", param: p, commandType: CommandType.StoredProcedure);
        }

        public User FindByUsername(string username)
        {
            var connection = _session.GetReadOnlyConnection();
            var p = new DynamicParameters();
            p.Add("inUsername", username);
            return connection.QueryFirstOrDefault<User>(sql: "dbo.API_GetUserByUsername", param: p, commandType: CommandType.StoredProcedure);
        }

        public User Update(User model)
        {
            var uom = _session.GetUnitOfWork();
            var connection = uom.GetConnection();
            var p = new DynamicParameters();
            p.Add("inId", model.Id);
            p.Add("inUsername", model.Username);
            p.Add("inPasswordHash", model.PasswordHash);
            p.Add("inPasswordSeed", model.PasswordSeed);
            p.Add("inToken", model.Token);
            p.Add("inExpiresIn", model.ExpiresIn);

            connection.Execute(sql: "dbo.API_UpdateUser", param: p, commandType: CommandType.StoredProcedure);

            return model;
        }
    }
}
