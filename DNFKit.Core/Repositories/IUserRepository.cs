using DNFKit.Core.Models;

namespace DNFKit.Core.Repositories
{
    public interface IUserRepository
    {
        User FindById(int id);
        User FindByUsername(string username);
        User Update(User model);
    }
}
