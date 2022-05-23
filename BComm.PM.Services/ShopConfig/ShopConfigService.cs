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
        private readonly ISubscriptionQueryRepository _subscriptionQueryRepository;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly string appDNSHost = "storepreview.bincommerz.com";
        private readonly string domainDNSHost = "storefront.bincommerz.com";

        private readonly string storePreviewAppName = "bcomm-store-preview-paperbag";
        private readonly string storeFrontAppName = "bcomm-store-front-paperbag";

        public ShopConfigService(
            IUrlMappingsQueryRepository urlMappingsQueryRepository,
            ICommandsRepository<UrlMappings> urlMappingsCommandRepository,
            IShopQueryRepository shopsQueryRepository,
            ISubscriptionQueryRepository subscriptionQueryRepository,
            IUserQueryRepository userQueryRepository,
            IMapper mapper)
        {
            _urlMappingsQueryRepository = urlMappingsQueryRepository;
            _urlMappingsCommandRepository = urlMappingsCommandRepository;
            _shopQueryRepository = shopsQueryRepository;
            _subscriptionQueryRepository = subscriptionQueryRepository;
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
                var urlMappingResponse = _mapper.Map<IEnumerable<DomainMappingResponse>>(urlMappings);
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

                var appDomain = userModel.UserName + ".bincommerz.com";
                var herokuDomain = await GetDomainCname(storePreviewAppName, appDomain);
                var appUrl = await GenerateDNSName(userModel.UserName, herokuDomain.cname);

                var newUrlMapping = new UrlMappings()
                {
                    HashId = Guid.NewGuid().ToString("N"),
                    CreatedOn = DateTime.UtcNow,
                    ShopId = shopId,
                    Url = appUrl.result.name,
                    Cname = herokuDomain.cname,
                    DnsId = appUrl.result.id,
                    CnameId = herokuDomain.id,
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
                var shop = await _shopQueryRepository.GetShopById(shopId);
                var subscription = await _subscriptionQueryRepository.GetSubscription(shop.UserHashId);

                if (!subscription.CanAddCustomDomain)
                {
                    throw new Exception("Not allowed for this action");
                }

                var existingDomain = await _urlMappingsQueryRepository.GetDomainByName(newDomainRequest.Url, shopId);

                if (existingDomain != null)
                {
                    throw new Exception("Domain already exists");
                }

                var herokuDomain = await GetDomainCname(storeFrontAppName, newDomainRequest.Url);

                var newUrlMapping = new UrlMappings()
                {
                    HashId = Guid.NewGuid().ToString("N"),
                    CreatedOn = DateTime.UtcNow,
                    ShopId = shopId,
                    Url = newDomainRequest.Url,
                    Cname = herokuDomain.cname,
                    CnameId = herokuDomain.id,
                    DnsId = null,
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

                await DeleteDomainCname(storeFrontAppName, existingDomain.CnameId);

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

        public async Task<Response> GetDomainUrls()
        {
            try
            {
                var urlMappings = await _urlMappingsQueryRepository.GetAllUrlMappingsListByType(UrlMapTypes.Domain);
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

        private async Task<HerokuDomainResponse> GetDomainCname(string herokuApp, string domain)
        {
            var dnsRequest = new HerokuDomainRequest
            {
                hostname = domain,
                sni_endpoint = null
            };

            var dnsData = JsonSerializer.Serialize(dnsRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.heroku.com/apps/" + herokuApp + "/domains");
            var mediaType = new MediaTypeWithQualityHeaderValue("application/vnd.heroku+json");
            mediaType.Parameters.Add(new NameValueHeaderValue("version", "3"));
            request.Headers.Accept.Add(mediaType);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "96247f0a-f52c-491e-a875-3c038b7ff712");
            request.Content = new StringContent(dnsData, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            // response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dnsResponse = JsonSerializer.Deserialize<HerokuDomainResponse>(content);

            return dnsResponse;
        }

        private async Task<HerokuDomainResponse> DeleteDomainCname(string herokuApp, string domainId)
        {
            var dnsRequest = new HerokuDomainRequest
            {
                sni_endpoint = null
            };

            var dnsData = JsonSerializer.Serialize(dnsRequest);

            var request = new HttpRequestMessage(HttpMethod.Delete, "https://api.heroku.com/apps/" + herokuApp + "/domains/" + domainId);
            var mediaType = new MediaTypeWithQualityHeaderValue("application/vnd.heroku+json");
            mediaType.Parameters.Add(new NameValueHeaderValue("version", "3"));
            request.Headers.Accept.Add(mediaType);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "96247f0a-f52c-491e-a875-3c038b7ff712");
            request.Content = new StringContent(dnsData, Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var dnsResponse = JsonSerializer.Deserialize<HerokuDomainResponse>(content);

            return dnsResponse;
        }
    }
}
