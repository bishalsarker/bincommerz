using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Auth;
using System.Threading.Tasks;

namespace BComm.PM.Services.Auth
{
    public interface IAuthService
    {
        string GetLoginRedirectUri(string client_id, string redirect_uri, Shop shop, string userName);
        Task<Shop> GetShop(string userName, string password);
        Task<Response> GetShopInfo(string shopId);
        Task<Response> UpdateShop(ShopUpdatePayload shopUpdateRequest, string shopId);
    }
}