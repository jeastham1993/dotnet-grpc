using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
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

        public OrderController(ILogger<OrderController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("{customerId}/order/{orderId}")]
        public async Task<IActionResult> GetSpecificCustomerOrder(string customerId, string orderId)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Orders.Orders.OrdersClient(channel);

            var order = await client.GetOrderAsync(new GetOrderRequest()
            {
                OrderId = orderId
            });

            var id = order.OrderId; // Checking to see that the deprecated warning appears.

            return this.Ok(order);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerOrders(string customerId)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Orders.Orders.OrdersClient(channel);

            var orders = await client.ListCustomerOrdersAsync(new GetCustomerOrdersRequest()
            {
                CustomerId = customerId
            });

            return this.Ok(orders);
        }
    }
}