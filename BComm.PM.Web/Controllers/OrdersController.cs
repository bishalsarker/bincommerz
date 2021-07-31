using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Common;
using BComm.PM.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderPaymentService _orderPaymentService;
        private readonly AuthService _authService;

        public OrdersController(
            IOrderService orderService, 
            IOrderPaymentService orderPaymentService,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderService = orderService;
            _orderPaymentService = orderPaymentService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllOrders(bool is_completed)
        {
            return Ok(await _orderService.GetAllOrders(_authService.GetShopId(), is_completed));
        }

        [HttpGet("get/incomplete")]
        [Authorize]
        public async Task<IActionResult> GetIncompletedOrders()
        {
            return Ok(await _orderService.GetAllOrders(_authService.GetShopId(), false));
        }

        [HttpGet("get/completed")]
        [Authorize]
        public async Task<IActionResult> GetCompletedOrders()
        {
            return Ok(await _orderService.GetAllOrders(_authService.GetShopId(), true));
        }

        [HttpGet("get/canceled")]
        [Authorize]
        public async Task<IActionResult> GetCanceledOrders()
        {
            return Ok(await _orderService.GetCanceledOrders(_authService.GetShopId()));
        }

        [HttpGet("get/{order_id}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(string order_id)
        {
            return Ok(await _orderService.GetOrder(order_id));
        }

        [HttpPatch("updateprocess")]
        [Authorize]
        public async Task<IActionResult> UpdateProcess(ProcessUpdatePayload processUpdateRequest)
        {
            return Ok(await _orderService.UpdateProcess(processUpdateRequest));
        }

        [HttpPatch("cancelorder")]
        [Authorize]
        public async Task<IActionResult> CancelOrder(OrderUpdatePayload orderUpdatePayload)
        {
            return Ok(await _orderService.CancelOrder(orderUpdatePayload));
        }

        [HttpPatch("completeorder")]
        [Authorize]
        public async Task<IActionResult> CompleteOrder(OrderUpdatePayload orderUpdatePayload)
        {
            return Ok(await _orderService.CompleteOrder(orderUpdatePayload));
        }

        [HttpDelete("deleteorder/{orderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(string orderId)
        {
            return Ok(await _orderService.DeleteOrder(orderId));
        }

        [HttpGet("payment/logs/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetPaymentLogs(string orderId)
        {
            return Ok(await _orderPaymentService.GetPaymentLogs(orderId));
        }

        [HttpPatch("payment/add")]
        [Authorize]
        public async Task<IActionResult> AddOrderPayment(OrderPaymentPayload newOrderPaymentRequest)
        {
            return Ok(await _orderPaymentService.AddPayment(newOrderPaymentRequest));
        }

        [HttpPatch("payment/deduct")]
        [Authorize]
        public async Task<IActionResult> DeductOrderPayment(OrderPaymentPayload newOrderPaymentRequest)
        {
            return Ok(await _orderPaymentService.DeductPayment(newOrderPaymentRequest));
        }
    }
}
