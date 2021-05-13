using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Orders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewOrder(OrderPayload newOrderRequest)
        {
            return Ok(await _orderService.AddNewOrder(newOrderRequest));
        }
    }
}
