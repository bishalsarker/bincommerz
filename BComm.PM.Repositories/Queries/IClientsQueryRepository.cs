using BComm.PM.Models.Auth;

namespace BComm.PM.Repositories.Queries
{
    public interface IClientsQueryRepository
    {
        Client GetClientById(string clientId);
    }
}