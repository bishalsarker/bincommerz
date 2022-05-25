using BComm.PM.Models.UrlMappings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IUrlMappingsQueryRepository
    {
        Task<IEnumerable<UrlMappings>> GetAllUrlMappingsListByType(UrlMapTypes mapType);
        Task<UrlMappings> GetDomainById(string domainId);
        Task<UrlMappings> GetDomainByName(string domainName, string shopId);
        Task<IEnumerable<UrlMappings>> GetUrlMappingsListByType(UrlMapTypes mapType, string shopId);
    }
}