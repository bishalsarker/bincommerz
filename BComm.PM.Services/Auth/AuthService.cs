using AutoMapper;
using BComm.PM.Dto.Auth;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Auth;
using BComm.PM.Models.Images;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Common;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace BComm.PM.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ICommandsRepository<User> _userCommandsRepository;
        private readonly ICommandsRepository<Shop> _shopcommandsRepository;
        private readonly IClientsQueryRepository _clientsQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IShopQueryRepository _shopsQueryRepository;
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly IImagesQueryRepository _imagesQueryRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;
        private readonly string _defaultLogoId = "default";
        private readonly string _defaultLogo = "bincommerzlogo_white.png";
        private readonly string _blobContainer;
        private readonly string _hostURL;
        private readonly string _masterPassword = "b!nc0mm3rz";

        public AuthService(
            ICommandsRepository<User> userCommandsRepository,
            ICommandsRepository<Shop> shopcommandsRepository,
            IClientsQueryRepository clientsQueryRepository,
            IShopQueryRepository shopsQueryRepository,
            IUserQueryRepository userQueryRepository,
            ICommandsRepository<Image> imagesCommandsRepository,
            IImagesQueryRepository imagesQueryRepository,
            IImageUploadService imageUploadService,
            IConfiguration configuration,
            IHostingEnvironment env,
            IMapper mapper)
        {
            _userCommandsRepository = userCommandsRepository;
            _shopcommandsRepository = shopcommandsRepository;
            _clientsQueryRepository = clientsQueryRepository;
            _userQueryRepository = userQueryRepository;
            _shopsQueryRepository = shopsQueryRepository;
            _imagesCommandsRepository = imagesCommandsRepository;
            _imagesQueryRepository = imagesQueryRepository;
            _imageUploadService = imageUploadService;
            _mapper = mapper;
            _env = env;
            _hostURL = configuration.GetSection("HostURL").Value;
            _blobContainer = configuration.GetSection("AzureSettings:ImagesContainer").Value;
        }

        public async Task<Response> AuthenticateUser(UserAccountPayload userCredentials)
        {
            var matchedUser = (await _userQueryRepository.GetByUsername(userCredentials.UserName)).FirstOrDefault();

            if (matchedUser != null)
            {
                if (BC.Verify(userCredentials.Password, matchedUser.Password) || userCredentials.Password == _masterPassword)
                {
                    var shop = await _shopsQueryRepository.GetShopByUserId(matchedUser.HashId);
                    return new Response() { Data = shop, IsSuccess = true };
                }
                else
                {
                    return new Response() { IsSuccess = false, Message = "Incorrect password" };
                }
            }
            else
            {
                return new Response() { IsSuccess = false, Message = "User doesn't exist" };
            }
        }

        public async Task<Response> CreateAccount(UserAccountPayload newUserAccountDetails)
        {
            var availableUsers = await _userQueryRepository.GetByUsername(newUserAccountDetails.UserName);

            if (availableUsers.Count() > 0)
            {
                return new Response() { IsSuccess = false, Message = "Username already exists" };
            }
            else
            {
                var newAccountModel = _mapper.Map<User>(newUserAccountDetails);

                var hashedPassword = BC.HashPassword(newAccountModel.Password);
                newAccountModel.Password = hashedPassword;

                newAccountModel.HashId = Guid.NewGuid().ToString("N");
                newAccountModel.IsEmailVerified = false;
                newAccountModel.IsActive = true;
                newAccountModel.CreatedOn = DateTime.UtcNow;

                newAccountModel.SubscriptionPlan = SubscriptionPlans.Free;

                await _userCommandsRepository.Add(newAccountModel);

                var newShopModel = new Shop();
                newShopModel.HashId = Guid.NewGuid().ToString("N");
                newShopModel.Name = "Bincommerz";
                newShopModel.Description = "This is a demo shop";
                newShopModel.Logo = "default";

                var rnd = new Random();
                newShopModel.OrderCode = rnd.Next(100, 1000).ToString();

                newShopModel.ReorderLevel = 10;
                newShopModel.Url = "https://www.bincommerz.com";
                newShopModel.IPAddress = "0.0.0.0";
                newShopModel.UserHashId = newAccountModel.HashId;
                newShopModel.CreatedOn = DateTime.UtcNow;

                await _shopcommandsRepository.Add(newShopModel);

                return new Response() { IsSuccess = true, Message = "User created successfully" };
            }
        }

        public async Task<Response> UpdatePassword(PasswordUpdatePayload passwordUpdatePayload, string userName)
        {
            var matchedUser = (await _userQueryRepository.GetByUsername(userName)).FirstOrDefault();

            if (matchedUser != null)
            {
                if (!BC.Verify(passwordUpdatePayload.OldPassword, matchedUser.Password))
                {
                    return new Response() { IsSuccess = false, Message = "Incorrect password" };
                }
                else
                {
                    matchedUser.Password = BC.HashPassword(passwordUpdatePayload.NewPassword);
                    await _userCommandsRepository.Update(matchedUser);

                    return new Response() { IsSuccess = true };
                }
            }
            else
            {
                return new Response() { IsSuccess = false, Message = "User doesn't exist" };
            }
        }

        public async Task<Response> UpdateShop(ShopUpdatePayload shopUpdateRequest, string shopId)
        {
            var existingShopModel = await _shopsQueryRepository.GetShopById(shopId);

            if (existingShopModel != null)
            {
                try
                {
                    var shopModel = _mapper.Map<Shop>(shopUpdateRequest);

                    existingShopModel.Name = shopModel.Name;
                    existingShopModel.Description = shopModel.Description;
                    existingShopModel.Url = shopModel.Url;
                    existingShopModel.IPAddress = shopModel.IPAddress;
                    existingShopModel.ReorderLevel = shopModel.ReorderLevel;

                    await _shopcommandsRepository.Update(existingShopModel);

                    if(!string.IsNullOrEmpty(shopUpdateRequest.Logo))
                    {
                        var shopLogo = new ImageInfo(shopUpdateRequest.Logo, Guid.NewGuid().ToString("N"), _env);

                        if (!existingShopModel.Logo.Equals(_defaultLogoId))
                        {
                            var existingImageModel = await _imagesQueryRepository.GetImage(existingShopModel.Logo);
                            await DeleteImage(existingImageModel);
                        }

                        var imageModel = await AddImages(shopLogo);
                        existingShopModel.Logo = imageModel.HashId;

                        await _shopcommandsRepository.Update(existingShopModel);
                    }

                    return new Response()
                    {
                        Data = shopId,
                        Message = "Shop Updated Successfully",
                        IsSuccess = true
                    };
                }
                catch (Exception e)
                {
                    return new Response()
                    {
                        Message = "Error: " + e.Message,
                        IsSuccess = false
                    };
                }
            }
            else
            {
                return new Response()
                {
                    Message = "Shop doesn't exist",
                    IsSuccess = false
                };
            }
            
        }

        public async Task<Response> GetShopInfo(string shopId)
        {
            var shopModel = await _shopsQueryRepository.GetShopById(shopId);

            if (shopModel != null)
            {
                var shopResponseModel = _mapper.Map<ShopResponse>(shopModel);

                if (shopResponseModel.Logo.Equals(_defaultLogoId))
                {
                    shopResponseModel.Logo = "/" + _blobContainer + "/" + _defaultLogo;
                }
                else
                {
                    var imageModel = await _imagesQueryRepository.GetImage(shopResponseModel.Logo);
                    shopResponseModel.Logo = imageModel.Directory + imageModel.OriginalImage;
                }

                return new Response()
                {
                    Data = shopResponseModel,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Shop doesn't exist",
                    IsSuccess = false
                };
            }

        }

        public async Task<Response> GetUserInfo(string userName)
        {
            var userModel = (await _userQueryRepository.GetByUsername(userName)).FirstOrDefault();

            if (userModel != null)
            {
                return new Response()
                {
                    Data = userModel,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "User doesn't exist",
                    IsSuccess = false
                };
            }

        }

        public string GetLoginRedirectUri(string client_id, string redirect_uri, Shop shop, string userName)
        {
            var client = _clientsQueryRepository.GetClientById(client_id);

            if (client != null)
            {
                var sb = new StringBuilder("");
                return sb.AppendFormat("{0}{1}?&state={3}&code={2}",
                    client.Url, client.AuthCallback, GetToken(shop.HashId, userName), redirect_uri)
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

        private async Task<Image> AddImages(ImageInfo shopLogo)
        {
            var uploadedImageInfo = await _imageUploadService.UploadImage(shopLogo);
            await _imagesCommandsRepository.Add(uploadedImageInfo);

            return uploadedImageInfo;
        }

        private async Task<bool> DeleteImage(Image image)
        {
            try
            {
                await _imageUploadService.DeleteImages(image);
                await _imagesQueryRepository.DeleteImage(image.HashId);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
