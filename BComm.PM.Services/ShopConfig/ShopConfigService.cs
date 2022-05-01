using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.UrlMappings;
using BComm.PM.Models.UrlMappings;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BComm.PM.Services.ShopConfig
{
    public class ShopConfigService : IShopConfigService
    {
        private readonly ICommandsRepository<UrlMappings> _urlMappingsCommandRepository;
        private readonly IUrlMappingsQueryRepository _urlMappingsQueryRepository;
        private readonly IShopQueryRepository _shopQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly string appDNSHost = "storepreview.bincommerz.com";
        private readonly string domainDNSHost = "storefront.bincommerz.com";

        public ShopConfigService(
            IUrlMappingsQueryRepository urlMappingsQueryRepository,
            ICommandsRepository<UrlMappings> urlMappingsCommandRepository,
            IShopQueryRepository shopsQueryRepository,
            IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _urlMappingsQueryRepository = urlMappingsQueryRepository;
            _urlMappingsCommandRepository = urlMappingsCommandRepository;
            _shopQueryRepository = shopsQueryRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetShopAllUrlMappings(string shopId)
        {
            try
            {
                var shopUrlMappingResponseModel = new ShopUrlMappingResponse();

                var appUrl = (await _urlMappingsQueryRepository.GetUrlMappingsListByType(UrlMapTypes.AppUrl, shopId)).FirstOrDefault();
                shopUrlMappingResponseModel.AppUrl = appUrl != null ? appUrl.Url : null;

                var urlMappings = await _urlMappingsQueryRepository.GetUrlMappingsListByType(UrlMapTypes.Domain, shopId);
                var urlMappingResponse = _mapper.Map<IEnumerable<UrlMappingResponse>>(urlMappings);
                shopUrlMappingResponseModel.Domains = urlMappingResponse;
                shopUrlMappingResponseModel.DomainDNSValue = domainDNSHost;

                return new Response()
                {
                    Data = shopUrlMappingResponseModel,
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

        public async Task<Response> AddAppUrl(string shopId)
        {
            try
            {
                var urlMappings = await _urlMappingsQueryRepository.GetUrlMappingsListByType(UrlMapTypes.AppUrl, shopId);

                if (urlMappings.Any())
                {
                    throw new Exception("App URL is already set");
                }

                var shopModel = await _shopQueryRepository.GetShopById(shopId);
                var userModel = await _userQueryRepository.GetById(shopModel.UserHashId);

                var appUrl = await GenerateDNSName(userModel.UserName, appDNSHost);

                var newUrlMapping = new UrlMappings()
                {
                    HashId = Guid.NewGuid().ToString("N"),
                    CreatedOn = DateTime.UtcNow,
                    ShopId = shopId,
                    Url = appUrl.result.name,
                    UrlMapType = UrlMapTypes.AppUrl
                };

                await _urlMappingsCommandRepository.Add(newUrlMapping);

                return new Response()
                {
                    Data = newUrlMapping.Url,
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

        public async Task<Response> AddDomain(UrlMappingPayload newDomainRequest, string shopId)
        {
            try
            {
                var existingDomain = await _urlMappingsQueryRepository.GetDomainByName(newDomainRequest.Url, shopId);

                if (existingDomain != null)
                {
                    throw new Exception("Domain already exists");
                }

                var newUrlMapping = new UrlMappings()
                {
                    HashId = Guid.NewGuid().ToString("N"),
                    CreatedOn = DateTime.UtcNow,
                    ShopId = shopId,
                    Url = newDomainRequest.Url,
                    UrlMapType = UrlMapTypes.Domain
                };

                await _urlMappingsCommandRepository.Add(newUrlMapping);

                return new Response()
                {
                    Data = newUrlMapping.Url,
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

        public async Task<Response> DeleteDomain(string domainId)
        {
            try
            {
                var existingDomain = await _urlMappingsQueryRepository.GetDomainById(domainId);

                if (existingDomain == null)
                {
                    throw new Exception("Domain doesn't exist");
                }

                await _urlMappingsCommandRepository.Delete(existingDomain);

                return new Response()
                {
                    Message = "Domain name deleted",
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

        public async Task<Response> GetAppUrls()
        {
            try
            {
                var urlMappings = await _urlMappingsQueryRepository.GetAllUrlMappingsListByType(UrlMapTypes.AppUrl);
                var urlMappingResponse = _mapper.Map<IEnumerable<UrlMappingResponse>>(urlMappings);

                return new Response()
                {
                    Data = urlMappingResponse,
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

        private async Task<CloudflareDNSResponse> GenerateDNSName(string appName, string dnsValue)
        {
            var dnsRequest = new CloudflareDNSPayload
            {
                type = "CNAME",
                name = appName,
                content = dnsValue,
                ttl = 3600,
                priority = 10,
                proxied = true
            };

            var dnsData = JsonSerializer.Serialize(dnsRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, 
                "https://api.cloudflare.com/client/v4/zones/6235d5f579569da1794e4d96173e703b/dns_records");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "flmng-9jsOg8JL69_EtX0dForfw6aTFtKq_EHkX0");
            request.Content = new StringContent(dnsData, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dnsResponse = JsonSerializer.Deserialize<CloudflareDNSResponse>(content);

            return dnsResponse;
        }
    }
}
