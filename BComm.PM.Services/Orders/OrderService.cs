using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Orders;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Processes;
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
        private readonly ICommandsRepository<OrderProcessLog> _orderProcessLogCommandsRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProcessQueryRepository _processQueryRepository;
        private readonly IShopQueryRepository _shopQueryRepository;
        private readonly IMapper _mapper;
        private readonly IDictionary<string, double> _deliveryChargeMap;

        public OrderService(
            ICommandsRepository<Order> orderCommandsRepository,
            ICommandsRepository<OrderItemModel> orderItemCommandsRepository,
            ICommandsRepository<OrderProcessLog> orderProcessLogCommandsRepository,
            IProductQueryRepository productQueryRepository,
            IOrderQueryRepository orderQueryRepository,
            IProcessQueryRepository processQueryRepository,
            IShopQueryRepository shopQueryRepository,
            IMapper mapper)
        {
            _orderCommandsRepository = orderCommandsRepository;
            _orderItemCommandsRepository = orderItemCommandsRepository;
            _productQueryRepository = productQueryRepository;
            _orderQueryRepository = orderQueryRepository;
            _processQueryRepository = processQueryRepository;
            _orderProcessLogCommandsRepository = orderProcessLogCommandsRepository;
            _shopQueryRepository = shopQueryRepository;
            _mapper = mapper;

            _deliveryChargeMap = new Dictionary<string, double>();
            _deliveryChargeMap.Add("potterybd", 150.00);
        }

        public async Task<Response> AddNewOrder(OrderPayload newOrderRequest, string shopId)
        {
            try
            {
                var shopModel = _shopQueryRepository.GetShopById(shopId);
                if (shopModel != null)
                {
                    var newOrderModel = _mapper.Map<Order>(newOrderRequest);
                    newOrderModel.HashId = GenerateOrderId(shopModel.OrderCode);
                    newOrderModel.ShopId = shopId;
                    newOrderModel.PlacedOn = DateTime.UtcNow;
                    newOrderModel.Status = "PENDING";

                    if (newOrderRequest.Items.Any())
                    {
                        await _orderCommandsRepository.Add(newOrderModel);

                        var productModels = await _productQueryRepository.GetProductsById(
                            newOrderRequest.Items.Select(x => x.ProductId).ToList(), shopId);

                        var totalPayable = 0.00;

                        foreach (var product in productModels)
                        {
                            var orderItemModel = _mapper.Map<OrderItemModel>(product);
                            orderItemModel.OrderId = newOrderModel.HashId;
                            var orderItemQuantity = newOrderRequest.Items
                                .FirstOrDefault(x => x.ProductId == product.HashId).Quantity;
                            orderItemModel.Quantity = orderItemQuantity;
                            await _orderItemCommandsRepository.Add(orderItemModel);
                            var discountAmount = product.Discount > 0 ? product.Price * (product.Discount / 100) : 0;
                            var productPrice = product.Price - discountAmount;
                            totalPayable = totalPayable + productPrice * orderItemQuantity;
                        }

                        var shippingCharge = GetShippingCharge(shopId);

                        newOrderModel.ShippingCharge = shippingCharge;
                        newOrderModel.TotalPayable = totalPayable + shippingCharge;
                        newOrderModel.TotalDue = totalPayable + shippingCharge;

                        await _orderCommandsRepository.Update(newOrderModel);

                        await _orderProcessLogCommandsRepository.Add(new OrderProcessLog()
                        {
                            OrderId = newOrderModel.HashId,
                            Title = "Order Placed",
                            Description = "Your have been placed and picked for processing",
                            LogDateTime = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        return new Response()
                        {
                            Message = "No items in order",
                            IsSuccess = false
                        };
                    }

                    return new Response()
                    {
                        Data = new { id = newOrderModel.HashId },
                        Message = "Order Placed Successfully",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Couldn't find specific shop",
                        IsSuccess = false
                    };
                }
                
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

        public async Task<Response> GetAllOrders(string shopId, bool isCompleted)
        {
            var ordersModel = await _orderQueryRepository.GetOrders(shopId, isCompleted);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<OrderResponse>>(ordersModel),
                IsSuccess = true
            };
        }

        public async Task<Response> GetCanceledOrders(string shopId)
        {
            var ordersModel = await _orderQueryRepository.GetCanceledOrders(shopId);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<OrderResponse>>(ordersModel),
                IsSuccess = true
            };
        }


        public async Task<Response> GetOrder(string orderId)
        {
            var ordersModel = await _orderQueryRepository.GetOrder(orderId);
            var orderResponse = _mapper.Map<OrderResponse>(ordersModel);
            var orderItems = await _orderQueryRepository.GetOrderItems(orderId);
            orderResponse.Items = _mapper.Map<IEnumerable<OrderItemResponse>>(orderItems);
            var processModel = await _processQueryRepository.GetProcess(ordersModel.CurrentProcessId);
            orderResponse.CurrentProcess = _mapper.Map<ProcessResponse>(processModel);

            return new Response()
            {
                Data = orderResponse,
                IsSuccess = true
            };
        }

        public async Task<Response> TrackOrder(string orderId)
        {
            var orderProcessLogsModel = await _orderQueryRepository.OrderLogs(orderId);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<OrderProcessLogResponse>>(orderProcessLogsModel),
                IsSuccess = true
            };
        }

        public async Task<Response> UpdateProcess(ProcessUpdatePayload processUpdateRequest)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(processUpdateRequest.OrderId);
                var processModel = await _processQueryRepository.GetProcess(processUpdateRequest.ProcessId);
                orderModel.CurrentProcessId = processModel.HashId;
                orderModel.Status = processModel.StatusCode;
                await _orderCommandsRepository.Update(orderModel);

                await _orderProcessLogCommandsRepository.Add(new OrderProcessLog()
                {
                    OrderId = orderModel.HashId,
                    Title = processModel.TrackingTitle,
                    Description = processModel.TrackingDescription,
                    LogDateTime = DateTime.UtcNow
                });

                return new Response()
                {
                    Message = processModel.Name + " started",
                    IsSuccess = true
                };
            }
            catch(Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> CancelOrder(OrderUpdatePayload orderUpdatePayload)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(orderUpdatePayload.OrderId);

                if (!orderModel.IsCanceled)
                {
                    orderModel.IsCanceled = true;
                    orderModel.Status = "CANCELED";

                    await _orderCommandsRepository.Update(orderModel);

                    await _orderProcessLogCommandsRepository.Add(new OrderProcessLog()
                    {
                        OrderId = orderModel.HashId,
                        Title = "Order Canceled",
                        Description = "",
                        LogDateTime = DateTime.UtcNow
                    });
                }

                return new Response()
                {
                    Message = "Order Canceled",
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

        public async Task<Response> CompleteOrder(OrderUpdatePayload orderUpdatePayload)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(orderUpdatePayload.OrderId);
                
                if(!orderModel.IsCompleted)
                {
                    orderModel.IsCompleted = true;
                    orderModel.Status = "COMPLETED";

                    await _orderCommandsRepository.Update(orderModel);

                    await _orderProcessLogCommandsRepository.Add(new OrderProcessLog()
                    {
                        OrderId = orderModel.HashId,
                        Title = "Order Delivered",
                        Description = "",
                        LogDateTime = DateTime.UtcNow
                    });
                }

                return new Response()
                {
                    Message = "Order Completed",
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

        public async Task<Response> DeleteOrder(string orderId)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(orderId);

                if (orderModel.IsCompleted || orderModel.IsCanceled)
                {
                    await _orderCommandsRepository.Delete(orderModel);
                    await _orderQueryRepository.DeleteOrderItems(orderModel.HashId);
                    await _orderQueryRepository.DeleteOrderProcessLogs(orderModel.HashId);
                    await _orderQueryRepository.DeleteOrderPaymentLogs(orderModel.HashId);

                    return new Response()
                    {
                        Message = "Order Deleted",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Order deletion is not allowed",
                        IsSuccess = false
                    };
                }
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

        private double GetShippingCharge(string shopId)
        {
            double result = 0.00;

            if (_deliveryChargeMap.TryGetValue(shopId, out result))
            {
                Console.WriteLine(result);
            }

            return result;
        }

        private string GenerateOrderId(string shopOrderCode)
        {
            var time_stamp = DateTime.UtcNow.Ticks.ToString();
            return shopOrderCode + "-" + time_stamp.Substring(time_stamp.Length / 2);
        }
    }
}
