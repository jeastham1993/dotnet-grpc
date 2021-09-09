// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using JEasthamDev.Grpc.DeliveryCalculator;
using JEasthamDev.Orders;

namespace JEasthamDev.Grpc.Orders.Services
{
	public class OrderService : JEasthamDev.Orders.Orders.OrdersBase
	{
		/// <inheritdoc />
		public async override Task<Order> GetOrder(GetOrderRequest request, ServerCallContext context)
		{
			var orderNumber = Guid.NewGuid().ToString();
			
			return new Order()
			{
				CustomerId = "test@test.com",
				OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
				OrderId = orderNumber,
				OrderNumber = orderNumber,
				DeliveryCharge = 10
			};
		}

		/// <inheritdoc />
		public async override Task<Order> CreateNewOrder(CreateOrderRequest request, ServerCallContext context)
		{
			using var channel = GrpcChannel.ForAddress("https://localhost:5012");
			var client = new Delivery.DeliveryClient(channel);

			var deliveryCharge = await client.CalculateAsync(new CalculateDeliveryRequest()
			{
				OrderValue = 0,
				Postcode = request.Postcode
			});
			
			var orderNumber = Guid.NewGuid().ToString();
			
			return new Order()
			{
				CustomerId = "test@test.com",
				OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
				OrderId = orderNumber,
				OrderNumber = orderNumber,
				DeliveryCharge = deliveryCharge.DeliveryCharge
			};
		}

		/// <inheritdoc />
		public async override Task<ListCustomerOrdersReply> ListCustomerOrders(GetCustomerOrdersRequest request, ServerCallContext context)
		{
			var orderNumber = Guid.NewGuid().ToString();
			var orders = new List<Order>(1)
			{
				new Order()
				{
					CustomerId = "test@test.com",
					OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
					OrderId = orderNumber,
					OrderNumber = orderNumber,
					DeliveryCharge = 10
				},
			};
			
			return new ListCustomerOrdersReply()
			{
				Order =
				{
					orders 
				}
			};
		}
	}
}