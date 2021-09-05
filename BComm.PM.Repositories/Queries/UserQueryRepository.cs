using BComm.PM.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BComm.PM.Repositories.Queries
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly List<User> _shops = new List<User>()
        {
            new User()
            {
                HashId = "e9fecdaad5ec4143a408d94f93706c20",
                UserName = "suchana.sarker",
                Password = "@binCommerz_123_1#"
            },

            new User()
            {
                HashId = "d7e28cad05184c749de26b4d6181621e",
                UserName = "dipu.paul",
                Password = "@binCommerz_123_1#"
            },
        };

        public User ValidateUser(string userName, string password)
        {
            return _shops.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }
    }
}
