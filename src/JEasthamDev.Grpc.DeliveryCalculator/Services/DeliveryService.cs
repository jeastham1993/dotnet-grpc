using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using JEasthamDev.Grpc.DeliveryCalculator;

namespace JEasthamDev.Grpc.DeliveryCalculator.Services
{
    public class DeliveryService : Delivery.DeliveryBase
    {
        private readonly ILogger<DeliveryService> _logger;
        public DeliveryService(ILogger<DeliveryService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public async override Task<CalculateDeliveryReply> Calculate(CalculateDeliveryRequest request, ServerCallContext context)
        {
            if (request.OrderValue > 100)
            {
                return new CalculateDeliveryReply()
                {
                    DeliveryCharge = 0F
                };
            }
            
            if (request.Postcode.StartsWith("BB3"))
            {
                return new CalculateDeliveryReply()
                {
                    DeliveryCharge = 10.00F
                };
            }
            
            return new CalculateDeliveryReply()
            {
                DeliveryCharge = 9.00F
            };
        }
    }
}