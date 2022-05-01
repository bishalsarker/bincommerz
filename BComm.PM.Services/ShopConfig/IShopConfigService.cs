using BComm.PM.Dto.Common;
using BComm.PM.Dto.UrlMappings;
using System.Threading.Tasks;

namespace BComm.PM.Services.ShopConfig
{
    public interface IShopConfigService
    {
        Task<Response> AddAppUrl(string shopId);
        Task<Response> AddDomain(UrlMappingPayload newDomainRequest, string shopId);
        Task<Response> DeleteDomain(string domainId);
        Task<Response> GetAppUrls();
        Task<Response> GetShopAllUrlMappings(string shopId);
    }
}