using BComm.PM.Models.Templates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ITemplateQueryRepository
    {
        Task<Template> GetDefaultTemplate(string shopId);
        Task<Template> GetTemplate(string shopId);
        Task<IEnumerable<Template>> GetTemplates(string shopId);
    }
}