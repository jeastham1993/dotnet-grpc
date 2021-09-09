using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using JEasthamDev.Grpc.RestProxy.Services;
using JEasthamDev.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JEasthamDev.Grpc.RestProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService             _orderService;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            this._logger = logger;
            this._orderService = orderService;
        }

        [HttpGet("{customerId}/order/{orderId}")]
        public async Task<IActionResult> GetSpecificCustomerOrder(string customerId, string orderId)
        {
            var order = await this._orderService.GetSpecificCustomerOrder(customerId, orderId);

            return this.Ok(order);
        }

        [HttpPost("{customerId}")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, string customerId)
        {
            request.CustomerId = customerId;
            
            var order = await this._orderService.CreateOrder(request);
            
            return this.Ok(order);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerOrders(string customerId)
        {
            var orders = await this._orderService.ListCustomerOrders(customerId);
            
            return this.Ok(orders);
        }
    }
}