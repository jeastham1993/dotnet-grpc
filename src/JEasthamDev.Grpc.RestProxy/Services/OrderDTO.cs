// ------------------------------------------------------------
// Copyright (c) James Eastham
// ------------------------------------------------------------

using System;

namespace JEasthamDev.Grpc.RestProxy.Services
{
	public class OrderDTO
	{
		public string OrderId { get; set; }
		
		public DateTime OrderDate { get; set; }
		
		public string CustomerId { get; set; }
		
		public float DeliveryCharge { get; set; }
	}
}