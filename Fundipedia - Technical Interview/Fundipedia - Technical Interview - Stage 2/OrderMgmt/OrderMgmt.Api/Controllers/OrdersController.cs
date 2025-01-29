using Microsoft.AspNetCore.Mvc;
using OrderMgmt.Domain;
using OrderMgmt.Domain.Services.Interfaces;

namespace OrderMgmt.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderProcessor orderProcessor) : ControllerBase
    {
        [HttpPost("process")]
        public ActionResult<OrderStatus> ProcessOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Invalid order request.");
            }

            var status = orderProcessor.DetermineOrderStatus(order);

            return Ok(status);
        }
    }
}
