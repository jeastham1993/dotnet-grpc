// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using JEasthamDev.Orders;

namespace JEasthamDev.Grpc.RestProxy.Services
{
	public class OrderService : IDisposable
	{
		private GrpcChannel                _grpcChannel;
		private Orders.Orders.OrdersClient _ordersClient;

		public OrderService()
		{
			this._grpcChannel = GrpcChannel.ForAddress("https://localhost:5001");
			this._ordersClient = new Orders.Orders.OrdersClient(this._grpcChannel);
		}

		public async Task<OrderDTO> CreateOrder(CreateOrderRequest request)
		{
			var orders = await this._ordersClient.CreateNewOrderAsync(new CreateOrderRequest()
			{
				CustomerId = request.CustomerId,
				Postcode = request.Postcode
			});

			return new OrderDTO()
			{
				CustomerId = orders.CustomerId,
				OrderDate = orders.OrderDate.ToDateTime(),
				DeliveryCharge = orders.DeliveryCharge,
				OrderId = orders.OrderNumber
			};
		}

		public async Task<OrderDTO> GetSpecificCustomerOrder(string customerId, string orderId)
		{
			var order = await this._ordersClient.GetOrderAsync(new GetOrderRequest()
			{
				OrderId = orderId
			});

			return new OrderDTO()
			{
				CustomerId = order.CustomerId,
				OrderDate = order.OrderDate.ToDateTime(),
				DeliveryCharge = order.DeliveryCharge,
				OrderId = order.OrderNumber
			};
		}

		public async Task<List<OrderDTO>> ListCustomerOrders(string customerId)
		{
			var orders = await this._ordersClient.ListCustomerOrdersAsync(new GetCustomerOrdersRequest()
			{
				CustomerId = customerId
			});

			var response = new List<OrderDTO>();

			foreach (var order in orders.Order)
			{
				response.Add(new OrderDTO()
				{
					CustomerId = order.CustomerId,
					OrderDate = order.OrderDate.ToDateTime(),
					DeliveryCharge = order.DeliveryCharge,
					OrderId = order.OrderNumber
				});
			}

			return response;
		}

		/// <inheritdoc />
		public void Dispose()
		{
			this._grpcChannel.Dispose();
		}
	}
}