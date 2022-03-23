using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Common;
using BComm.PM.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("orders/settings/delivery-charge")]
    [ApiController]
    public class DeliveryChargeController : ControllerBase
    {
        private readonly IDeliveryChargeService _deliveryChargeService;
        private readonly AuthService _authService;

        public DeliveryChargeController(IDeliveryChargeService deliveryChargeController, IHttpContextAccessor httpContextAccessor)
        {
            _deliveryChargeService = deliveryChargeController;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpPost("addnew")]
        [Authorize]
        public async Task<IActionResult> AddNewDeliveryCharge(DeliveryChargePayload deliveryChargePayload)
        {
            return Ok(await _deliveryChargeService.AddNewDeliveryCharge(deliveryChargePayload, _authService.GetShopId()));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateDeliveryCharge(DeliveryChargeUpdatePayload deliveryChargeUpdatePayload)
        {
            return Ok(await _deliveryChargeService.UpdateDeliveryCharge(deliveryChargeUpdatePayload));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllDeliveryCharges()
        {
            return Ok(await _deliveryChargeService.GetAllDeliveryCharges(_authService.GetShopId()));
        }

        [HttpGet("get/{deliveryChargeId}")]
        [Authorize]
        public async Task<IActionResult> GetDeliveryCharge(string deliveryChargeId)
        {
            return Ok(await _deliveryChargeService.GetDeliveryCharge(deliveryChargeId));
        }

        [HttpDelete("delete/{deliveryChargeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteDeliveryCharge(string deliveryChargeId)
        {
            return Ok(await _deliveryChargeService.DeleteDeliveryCharge(deliveryChargeId));
        }
    }
}
