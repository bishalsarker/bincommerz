using BComm.PM.Models.Auth;
using BComm.PM.Repositories.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BComm.PM.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IClientsQueryRepository _clientsQueryRepository;
        private readonly IShopQueryRepository _shopsQueryRepository;
        private readonly string _hostURL;

        public AuthService(
            IClientsQueryRepository clientsQueryRepository,
            IShopQueryRepository shopsQueryRepository,
            IConfiguration configuration)
        {
            _clientsQueryRepository = clientsQueryRepository;
            _shopsQueryRepository = shopsQueryRepository;
            _hostURL = configuration.GetSection("HostURL").Value;
        }

        public Shop GetShop(string userName, string password)
        {
            return _shopsQueryRepository.FindShop(userName, password);
        }

        public string GetLoginRedirectUri(string client_id, string redirect_uri, Shop shop)
        {
            var client = _clientsQueryRepository.GetClientById(client_id);

            if (client != null)
            {
                var sb = new StringBuilder("");
                return sb.AppendFormat("{0}{1}?&state={3}&code={2}",
                    client.Url, client.AuthCallback, GetToken(shop.ShopId, shop.UserName), redirect_uri)
                    .ToString();
            }
            else
            {
                return _hostURL;
            }
        }

        private string GetToken(string shopId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("PDv7DrqznYL6nv7DrqzjnQYO9JxIsWdcjnQYL6nu0f");
            var signinKey = new SymmetricSecurityKey(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "bincommerz-auth",
                Audience = "bincommerz-clients",
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, shopId)
                }),

                Expires = DateTime.UtcNow.AddDays(15)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
