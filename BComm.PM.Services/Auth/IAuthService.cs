using BComm.PM.Models.Auth;

namespace BComm.PM.Services.Auth
{
    public interface IAuthService
    {
        string GetLoginRedirectUri(string client_id, string redirect_uri, Shop shop);
        Shop GetShop(string userName, string password);
    }
}