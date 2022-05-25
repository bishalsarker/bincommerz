using BComm.PM.Models.Subscriptions;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Subscriptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.MethodAttributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SubscriptionCheckAttribute : ActionFilterAttribute
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionCheckAttribute(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var claims = context.HttpContext.User.Claims;
                var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

                var subscription = await _subscriptionService.GetSubscription(shopId);

                if (subscription == null)
                {
                    throw new Exception("Invalid Subscription");
                }

                if (subscription.IsActive)
                {
                    await next();
                }
                else
                {
                    context.Result = new BadRequestObjectResult(
                        new {
                            isSuccess = false,
                            message = "Action is not allowed. Subscription is not active." 
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult(new { message = ex.Message });
            }
        }
    }
}
