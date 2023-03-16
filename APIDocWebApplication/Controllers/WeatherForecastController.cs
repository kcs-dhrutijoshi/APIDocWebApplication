using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDocWebApplication.Controllers
{
    /// <summary>
    /// Weather Forecasts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Get Weather forecast and place orders. Very weird and unstructed API :)")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Constructor for Dependency Injection
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Return 5 random weather forecasts
        /// </summary>
        /// <remarks>
        /// This endpoint will return 5 days of weather forecasts with random temperatures in celcius.
        /// </remarks>
        /// <returns>5 Weather forecasts</returns>
        /// <response code="200">Returns the weather forecasts</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [SwaggerOperation(
            Summary = "Get Weather Forecast",
            Description = "This endpoint will return 5 days of weather forecasts with random temperatures in celcius.",
            OperationId = "Get",
            Tags = new[] { "WeatherForecast" })]
        [SwaggerResponse(200, "The random weather forecasts", typeof(WeatherForecast))]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("AddOrder")]
        [SwaggerOperation(
            Summary = "Add a new order to the API",
            Description = "This endpoint will take in a new order and return it to the client.",
            OperationId = "AddOrder",
            Tags = new[] { "Order" })]
        [SwaggerResponse(200, "The posted order payload", type: typeof(Order))]
        public ActionResult<Order> AddOrder([FromBody, SwaggerRequestBody("The order payload", Required = true)] Order order)
        {
            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, order);
        }

        /// <summary>
        /// Get an order by Order ID
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>The order object</returns>
        [HttpGet("GetOrder")]
        [SwaggerOperation(
            Summary = "Get an order by Order ID",
            Description = "Use the endpoint to request an order by it's Order ID.",
            OperationId = "GetOrder",
            Tags = new[] { "Order" })]
        [SwaggerResponse(200, "The requested order", type: typeof(Order))]
        public ActionResult<Order> GetOrder([FromQuery, SwaggerParameter("Order ID", Required = true)] int orderId)
        {
            List<Order> orders = new List<Order>();

            orders.Add(new Order
            {
                Id = 1,
                OrderId = 8427,
                CustomerName = "Christian Schou",
                Address = "Some Address here",
                OrderValue = "87429,8236 DKK"
            });
            orders.Add(new Order
            {
                Id = 1,
                OrderId = 3265,
                CustomerName = "John Doe",
                Address = "Johns address here",
                OrderValue = "236,255 DKK"
            });

            return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status200OK, orders.FirstOrDefault(x => x.OrderId == orderId));
        }
    }
}
