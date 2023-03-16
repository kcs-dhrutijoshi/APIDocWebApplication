using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDocWebApplication
{
    public class Order
    {
        [SwaggerSchema(
            Title = "Unique ID",
            Description = "This is the database ID and will be unique.",
            Format = "int")]
        public int Id { get; set; }

        [SwaggerSchema(
            Title = "Order ID",
            Description = "This is the Order ID, identifying the specific order.",
            Format = "int")]
        public int OrderId { get; set; }

        [SwaggerSchema(
            Title = "Customer Full Name",
            Description = "Full name for customer placing the order.",
            Format = "string")]
        public string? CustomerName { get; set; }

        [SwaggerSchema(
            Title = "Customer Address",
            Description = "Please include all details about customer address in this string.",
            Format = "string")]
        public string? Address { get; set; }

        [SwaggerSchema(
            Title = "Total Order Value",
            Description = "Sub. Total Value for order placed by customer. Should have been a double :)",
            Format = "string")]
        public string? OrderValue { get; set; }
    }
}
