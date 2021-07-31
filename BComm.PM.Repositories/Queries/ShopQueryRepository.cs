using BComm.PM.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BComm.PM.Repositories.Queries
{
    public class ShopQueryRepository : IShopQueryRepository
    {
        private readonly List<Shop> _shops = new List<Shop>()
        {
            new Shop()
            {
                UserName = "suchana.sarker",
                Password = "@binCommerz_123_1#",
                ShopId = "vbt_xyz",
                OrderCode = "101"
            },

            new Shop()
            {
                UserName = "dipu.paul",
                Password = "@binCommerz_123_1#",
                ShopId = "potterybd",
                OrderCode = "237"
            },
        };

        public Shop FindShop(string userName, string password)
        {
            return _shops.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }

        public Shop GetShopById(string shopId)
        {
            return _shops.FirstOrDefault(x => x.ShopId == shopId);
        }
    }
}
