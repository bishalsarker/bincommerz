using BComm.PM.Dto.Common;
using BComm.PM.Dto.Template;
using System.Threading.Tasks;

namespace BComm.PM.Services.Templates
{
    public interface ITemplateService
    {
        Task<Response> AddTemplate(TemplatePayload templateUpdateRequest, string shopId);
        Task<Response> GetAllTemplates(string shopId);
        Task<Response> GetDefaultTemplate(string shopId);
        Task<Response> GetTemplate(string templateId);
        Task<Response> UpdateTemplate(TemplateUpdatePayload templateUpdateRequest);
    }
}