using Microsoft.AspNetCore.Mvc;
using OrderMgmt.Api.Services.Interfaces;

namespace OrderMgmt.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IOrderProcessor orderProcessor) : ControllerBase
    {
    }
}
