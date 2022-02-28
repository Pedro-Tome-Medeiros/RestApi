using System.Collections.Generic;
using System.Threading.Tasks;
using RestApi.Models;

namespace RestApi.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUser(int id);

        Task<bool> Update(User user, int id);

        Task<User> Create(User user);

        Task<bool> Delete(int id);
    }
}
