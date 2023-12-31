﻿using Infraestructure.Kafka.Producer;
using System.Text.Json;

namespace N5_challenge.Middlewares
{
    public class KafkaManagementMiddleware : IMiddleware
    {
        private readonly KafkaProducerWrapper<string, string> _kafkaProducer;

        public KafkaManagementMiddleware(KafkaProducerWrapper<string, string> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var operationMethod = context.Request.Path.ToString().Split('/').Last();
            if (operationMethod.Equals("{id}"))
            {
                operationMethod = context.Request.Path.ToString().Split('/').SkipLast(1).Last();
            }
            string operationName = operationMethod switch
            {
                "GetPermissions" => "get",
                "RequestPermission" => "request",
                "ModifyPermission{id}" => "modify",
                _ => "operation invalid"
            };

            var message = new { id = Guid.NewGuid(), nameOperation = operationName };
            await _kafkaProducer.ProduceAsync("permission", "operation", JsonSerializer.Serialize(message));
            await next(context);
        }
    }
}
