using BComm.PM.Models.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IUserQueryRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetById(string userId);
        Task<IEnumerable<User>> GetByUsername(string userName);
        Task<User> GetByUserNamePassword(string userName, string password);
    }
}