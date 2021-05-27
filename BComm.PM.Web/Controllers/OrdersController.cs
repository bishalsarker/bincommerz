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

        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewOrder(OrderPayload newOrderRequest, [FromHeader] string shop_id)
        {
            return Ok(await _orderService.AddNewOrder(newOrderRequest, shop_id));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllOrders(bool is_completed)
        {
            return Ok(await _orderService.GetAllOrders(_authService.GetShopId(), is_completed));
        }

        [HttpGet("get/{order_id}")]
        [Authorize]
        public async Task<IActionResult> GetOrder(string order_id)
        {
            return Ok(await _orderService.GetOrder(order_id));
        }

        [HttpGet("track/{order_id}")]
        public async Task<IActionResult> TrackOrder(string order_id)
        {
            return Ok(await _orderService.TrackOrder(order_id));
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
