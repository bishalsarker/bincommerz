using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Orders;
using BComm.PM.Models.Products;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICommandsRepository<Order> _orderCommandsRepository;
        private readonly ICommandsRepository<OrderItemModel> _orderItemCommandsRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public OrderService(
            ICommandsRepository<Order> orderCommandsRepository,
            ICommandsRepository<OrderItemModel> orderItemCommandsRepository,
            IProductQueryRepository productQueryRepository,
            IMapper mapper)
        {
            _orderCommandsRepository = orderCommandsRepository;
            _orderItemCommandsRepository = orderItemCommandsRepository;
            _productQueryRepository = productQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewOrder(OrderPayload newOrderRequest)
        {
            try
            {
                var newOrderModel = _mapper.Map<Order>(newOrderRequest);
                newOrderModel.HashId = Guid.NewGuid().ToString().ToUpper();
                newOrderModel.ShopId = "vbt_xyz";
                newOrderModel.PlacedOn = DateTime.UtcNow;

                await _orderCommandsRepository.Add(newOrderModel);

                var productModels = await _productQueryRepository.GetProductsById(
                    newOrderRequest.Items.Select(x => x.ProductId).ToList(), "vbt_xyz");

                var totalPayable = 0.00;

                foreach (var product in productModels)
                {
                    var orderItemModel = _mapper.Map<OrderItemModel>(product);
                    var orderItemQuantity = newOrderRequest.Items
                        .FirstOrDefault(x => x.ProductId == product.HashId).Quantity;
                    orderItemModel.Quantity = orderItemQuantity;
                    await _orderItemCommandsRepository.Add(orderItemModel);
                    var discountAmount = product.Discount > 0 ? product.Price * (product.Discount / 100) : 0;
                    var productPrice = product.Price - discountAmount;
                    totalPayable = totalPayable + productPrice * orderItemQuantity;
                }

                newOrderModel.TotalPayable = totalPayable + 70; // shipping charge (70) addition

                await _orderCommandsRepository.Update(newOrderModel);

                return new Response()
                {
                    Data = new { id = newOrderModel.HashId },
                    Message = "Order Placed Successfully",
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }
    }
}
