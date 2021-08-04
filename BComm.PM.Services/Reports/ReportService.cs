using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Orders;
using BComm.PM.Dto.Reports;
using BComm.PM.Dto.Tags;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IShopQueryRepository _shopQueryRepository;
        private readonly IMapper _mapper;

        public ReportService(
            IProductQueryRepository productQueryRepository,
            IOrderQueryRepository orderQueryRepository,
            IShopQueryRepository shopQueryRepository,
            ITagsQueryRepository tagsQueryRepository,
            IMapper mapper)
        {
            _productQueryRepository = productQueryRepository;
            _orderQueryRepository = orderQueryRepository;
            _shopQueryRepository = shopQueryRepository;
            _tagsQueryRepository = tagsQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetOrderSummary(string shopId)
        {
            try
            {
                var totalOrderCount = await _orderQueryRepository.GetAllOrderCount(shopId);
                var totalCompletedOrderCount = await _orderQueryRepository.GetOrderCountByStatus(shopId, true);
                var totalInCompletedOrderCount = await _orderQueryRepository.GetOrderCountByStatus(shopId, false);
                var totalCanceledOrderCount = await _orderQueryRepository.GetCanceledOrderCount(shopId);

                return new Response()
                {
                    Data = new OrderSummaryResponse()
                    {
                        TotalOrder = totalOrderCount.FirstOrDefault(),
                        TotalCompletedOrder = totalCompletedOrderCount.FirstOrDefault(),
                        TotalIncompleteOrder = totalInCompletedOrderCount.FirstOrDefault(),
                        TotalCanceledOrder = totalCanceledOrderCount.FirstOrDefault()
                    },

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

        public async Task<Response> GetMostOrderedProducts(string shopId, int month, int year)
        {
            try
            {
                var productsModel = await _productQueryRepository.GetAllProducts(shopId);
                var orderCount = await _orderQueryRepository.GetOrdersByMonthAndYear(
                    productsModel.Select(x => x.HashId).ToList(), shopId, month, year, 10);

                var ordersModel = await _orderQueryRepository.GetAllOrders(shopId);
                var totalOrderCount = await _orderQueryRepository.GetOrderCountByItems(ordersModel.Select(x => x.HashId).ToList());

                return new Response()
                {
                    Data = new OrderCountResponse()
                    {
                        TotalOrderCount = totalOrderCount.FirstOrDefault(),
                        OrderCounts = orderCount.OrderByDescending((x => x.OrderCount))
                    },

                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetMostPopularTags(string shopId, int month, int year)
        {
            try
            {
                var productsModel = await _productQueryRepository.GetAllProducts(shopId);
                var orderCountList = await _orderQueryRepository.GetOrdersByMonthAndYear(
                    productsModel.Select(x => x.HashId).ToList(), shopId, month, year, 10);

                var ordersModel = await _orderQueryRepository.GetAllOrders(shopId);
                var totalOrderCount = await _orderQueryRepository.GetOrderCountByItems(ordersModel.Select(x => x.HashId).ToList());

                var tagsCountResponse = new List<TagsPopularityResponse>();

                foreach (var orderCount in orderCountList)
                {
                    var tagsModel = await _tagsQueryRepository.GetTagsByProductId(orderCount.ProductId);
                    foreach (var tag in tagsModel)
                    {
                        if (!tagsCountResponse.Any(x => x.TagId == tag.TagHashId))
                        {
                            tagsCountResponse.Add(new TagsPopularityResponse()
                            {
                                TagId = tag.TagHashId,
                                TagName = tag.TagName,
                                Percentage = ((double)orderCount.OrderCount / (double)totalOrderCount.FirstOrDefault()) * 100
                            });
                        }
                    }
                }

                return new Response()
                {
                    Data = tagsCountResponse,
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
    }
}
