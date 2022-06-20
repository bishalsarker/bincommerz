using BComm.PM.Dto.Common;
using System.Threading.Tasks;

namespace BComm.PM.Services.Admin
{
    public interface IAdminService
    {
        Task<Response> GetUsers();
    }
}