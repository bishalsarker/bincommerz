using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Orders;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Orders;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public class OrderPaymentService : IOrderPaymentService
    {
        private readonly ICommandsRepository<OrderPaymentLog> _orderPaymentLogCommandsRepository;
        private readonly ICommandsRepository<Order> _orderCommandsRepository;
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly IMapper _mapper;

        public OrderPaymentService(
            ICommandsRepository<OrderPaymentLog> orderPaymentLogCommandsRepository,
            ICommandsRepository<Order> orderCommandsRepository,
            IOrderQueryRepository orderQueryRepository,
            IMapper mapper)
        {
            _orderPaymentLogCommandsRepository = orderPaymentLogCommandsRepository;
            _orderCommandsRepository = orderCommandsRepository;
            _orderQueryRepository = orderQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> GetPaymentLogs(string orderId)
        {
            var orderPaymentLogsModel = await _orderQueryRepository.OrderPaymentLogs(orderId);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<OrderPaymentLogResponse>>(orderPaymentLogsModel),
                IsSuccess = true
            };
        }

        public async Task<Response> AddPayment(OrderPaymentPayload newOrderPaymentRequest)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(newOrderPaymentRequest.OrderId);

                if (orderModel.TotalDue > 0)
                {
                    var currentDue = orderModel.TotalDue;

                    if (currentDue >= newOrderPaymentRequest.Amount)
                    {
                        orderModel.TotalDue = currentDue - newOrderPaymentRequest.Amount;
                        await _orderCommandsRepository.Update(orderModel);
                        await LogPayment(newOrderPaymentRequest, "deduct");
                    }
                }

                return new Response()
                {
                    Message = "Order Payment Updated",
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

        public async Task<Response> DeductPayment(OrderPaymentPayload newOrderPaymentRequest)
        {
            try
            {
                var orderModel = await _orderQueryRepository.GetOrder(newOrderPaymentRequest.OrderId);

                var currentDue = orderModel.TotalDue;
                var totalDeduction = currentDue + newOrderPaymentRequest.Amount;

                if (orderModel.TotalPayable >= totalDeduction)
                {
                    orderModel.TotalDue = currentDue + newOrderPaymentRequest.Amount;
                    await _orderCommandsRepository.Update(orderModel);
                    await LogPayment(newOrderPaymentRequest, "deduct");
                }

                return new Response()
                {
                    Message = "Order Payment Updated",
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

        private async Task LogPayment(OrderPaymentPayload newOrderPaymentRequest, string transactionType)
        {
            var orderPaymentLogModel = _mapper.Map<OrderPaymentLog>(newOrderPaymentRequest);
            orderPaymentLogModel.TransactionType = transactionType;
            orderPaymentLogModel.LogDateTime = DateTime.UtcNow;
            await _orderPaymentLogCommandsRepository.Add(orderPaymentLogModel);
        }
    }
}
