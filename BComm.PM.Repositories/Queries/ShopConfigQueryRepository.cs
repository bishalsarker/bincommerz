using BComm.PM.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BComm.PM.Repositories.Queries
{
    public class ShopConfigQueryRepository : IShopConfigQueryRepository
    {
        private readonly List<ShopConfig> _shopConfigs = new List<ShopConfig>()
        {
            new ShopConfig()
            {
                ShopId = "vbt_xyz",
                OrderCode = "101",
                ReorderLevel = 3,
            },

            new ShopConfig()
            {
                ShopId = "potterybd",
                OrderCode = "237",
                ReorderLevel = 3
            },
        };

        public ShopConfig GetShopConfigById(string shopId)
        {
            return _shopConfigs.FirstOrDefault(x => x.ShopId == shopId);
        }
    }
}
