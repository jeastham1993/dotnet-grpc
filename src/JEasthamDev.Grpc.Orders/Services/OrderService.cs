// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using JEasthamDev.Orders;

namespace JEasthamDev.Grpc.Orders.Services
{
	public class OrderService : JEasthamDev.Orders.Orders.OrdersBase
	{
		/// <inheritdoc />
		public async override Task<GetOrderReply> GetOrder(GetOrderRequest request, ServerCallContext context)
		{
			var orderNumber = Guid.NewGuid().ToString();
			
			return new GetOrderReply()
			{
				CustomerId = "test@test.com",
				OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
				OrderId = orderNumber,
				OrderNumber = orderNumber
			};
		}

		/// <inheritdoc />
		public async override Task<ListCustomerOrdersReply> ListCustomerOrders(GetCustomerOrdersRequest request, ServerCallContext context)
		{
			var orderNumber = Guid.NewGuid().ToString();
			var orders = new List<GetOrderReply>(1)
			{
				new GetOrderReply()
				{
					CustomerId = "test@test.com",
					OrderDate = Timestamp.FromDateTime(DateTime.UtcNow),
					OrderId = orderNumber,
					OrderNumber = orderNumber
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