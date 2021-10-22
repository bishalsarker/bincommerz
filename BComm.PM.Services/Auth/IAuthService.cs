using BComm.PM.Dto.Auth;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Auth;
using System.Threading.Tasks;

namespace BComm.PM.Services.Auth
{
    public interface IAuthService
    {
        Task<Response> AuthenticateUser(UserAccountPayload userCredentials);
        string GetLoginRedirectUri(string client_id, string redirect_uri, Shop shop, string userName);
        Task<Response> GetShopInfo(string shopId);
        Task<Response> UpdatePassword(PasswordUpdatePayload passwordUpdatePayload, string userName);
        Task<Response> UpdateShop(ShopUpdatePayload shopUpdateRequest, string shopId);
    }
}