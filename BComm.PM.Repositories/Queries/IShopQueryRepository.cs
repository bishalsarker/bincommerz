using BComm.PM.Models.Auth;

namespace BComm.PM.Repositories.Queries
{
    public interface IShopQueryRepository
    {
        Shop FindShop(string userName, string password);
        Shop GetShopById(string shopId);
    }
}