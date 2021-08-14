using BComm.PM.Models.Auth;

namespace BComm.PM.Repositories.Queries
{
    public interface IShopConfigQueryRepository
    {
        ShopConfig GetShopConfigById(string shopId);
    }
}