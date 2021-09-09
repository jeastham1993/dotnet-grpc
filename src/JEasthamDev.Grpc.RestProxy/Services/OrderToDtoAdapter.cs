// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System.Runtime.CompilerServices;
using JEasthamDev.Orders;

namespace JEasthamDev.Grpc.RestProxy.Services
{
	public class OrderToDtoAdapter : OrderDTO
	{
		private Order _order;
		
		public OrderToDtoAdapter(Order order)
		{
			this._order = order;
		}

		public OrderDTO GetDto()
		{
			return new OrderDTO()
			{
				CustomerId = this._order.CustomerId,
				OrderDate = this._order.OrderDate.ToDateTime(),
				DeliveryCharge = this._order.DeliveryCharge,
				OrderId = this._order.OrderNumber
			};
		}
	}
}