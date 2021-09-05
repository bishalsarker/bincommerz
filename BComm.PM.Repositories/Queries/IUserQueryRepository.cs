using BComm.PM.Models.Auth;

namespace BComm.PM.Repositories.Queries
{
    public interface IUserQueryRepository
    {
        User ValidateUser(string userName, string password);
    }
}