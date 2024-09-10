using BComm.PM.Dto.Common;
using BComm.PM.Dto.User;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public AdminService(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async Task<Response> GetUsers()
        {
            var userModels = await _userQueryRepository.GetAllUsers();
            var userResponse = userModels.Select(user => new UserResponse() { 
                UserName = user.UserName, Email = user.Email 
            });

            return new Response()
            {
                Data = userResponse,
                IsSuccess = true
            };

        }
    }
}
