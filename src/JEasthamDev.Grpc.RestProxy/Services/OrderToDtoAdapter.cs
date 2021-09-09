// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System.Runtime.CompilerServices;
using JEasthamDev.Orders;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace JEasthamDev.Grpc.RestProxy.Services
{
	public class OrderToDtoAdapter : OrderDTO
	{
		public OrderToDtoAdapter(Order order)
		{
			this.CustomerId = order.CustomerId;
			this.OrderDate = order.OrderDate.ToDateTime();
			this.DeliveryCharge = order.DeliveryCharge;
			this.OrderId = order.OrderNumber;
		}
	}
}