using BComm.PM.Dto.Common;
using System.Threading.Tasks;

namespace BComm.PM.Services.Reports
{
    public interface IReportService
    {
        Task<Response> GetMostOrderedProducts(string shopId, int month, int year);
        Task<Response> GetMostPopularTags(string shopId, int month, int year);
        Task<Response> GetOrderSummary(string shopId);
    }
}