using BComm.PM.Models.Auth;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IShopQueryRepository
    {
        Task<Shop> GetShopByUserId(string userId);
        Task<Shop> GetShopById(string shopId);
    }
}