using BComm.PM.Dto.Common;
using System.Threading.Tasks;

namespace BComm.PM.Services
{
    public interface IProcessService
    {
        Task<Response> GetNextProcess(string shopId, int currentStep);
    }
}