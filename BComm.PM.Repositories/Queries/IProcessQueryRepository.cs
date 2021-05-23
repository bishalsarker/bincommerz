using BComm.PM.Models.Processes;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IProcessQueryRepository
    {
        Task<Process> GetNextProcess(string shopId, int currentStep);
        Task<Process> GetProcess(string processId);
    }
}