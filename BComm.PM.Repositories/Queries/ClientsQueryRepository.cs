using BComm.PM.Models.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BComm.PM.Repositories.Queries
{
    public class ClientsQueryRepository : IClientsQueryRepository
    {
        private readonly string _hostURL;

        public ClientsQueryRepository(IConfiguration configuration)
        {
            _hostURL = configuration.GetSection("HostURL").Value;
        }

        public Client GetClientById(string clientId)
        {
            return GetClients().FirstOrDefault(x => x.Id == clientId);
        }

        private List<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    Id = "bcomm_om",
                    Url = _hostURL + "retail-admin/order-management/",
                    AuthCallback = "#/auth-callback"
                },
                new Client()
                {
                    Id = "bcomm_pm",
                    Url = _hostURL + "retail-admin/product-management/",
                    AuthCallback = "#/auth-callback"
                },
                new Client()
                {
                    Id = "bcomm_portal",
                    Url = _hostURL + "retail-admin/portal/",
                    AuthCallback = "#/auth-callback"
                }
            };
        }
    }
}
