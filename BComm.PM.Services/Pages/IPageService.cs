using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Pages
{
    public interface IPageService
    {
        Task<Response> AddPage(PagePayload newPageRequest, string shopId);
        Task<Response> DeletePage(string pageId);
        Task<Response> GetAllPages(string shopId);
        Task<Response> GetAllPagesSorted(string shopId);
        Task<Response> GetPageBySlug(string category, string slug, string shopId);
        Task<Response> GetPagesByCategory(string category, string shopId);
        Task<Response> UpdatePage(PagePayload newPageRequest);
        Task<Response> GetPage(string pageId);
    }
}