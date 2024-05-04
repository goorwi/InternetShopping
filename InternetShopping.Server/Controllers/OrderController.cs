using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ShopLibrary;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var orders = new OrderBD().Read();
            return Ok(orders);
        }

        [HttpGet("get")]
        public IActionResult GetCur(int CustomerId, int Cost, string OrderStatus)
        {
            var ord = new OrderBD().SearchBy(CustomerId, Cost, OrderStatus);
            if (ord == null)
                return BadRequest();
            return Ok(ord);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var order = new OrderBD().SearchById(Id);

            if (order == null) 
                return BadRequest();
            else
                return Ok(order);
        }

        [HttpPost("add_order")]
        public IActionResult Post([FromBody] Order order)
        {
            if (order.OrderDate == null || order.OrderStatus== null || order.CustomerId < 0 || order.Cost < 0)
                return BadRequest();

            var customer = new CustomerBD().SearchById(order.CustomerId);
            if (customer == null)
                return BadRequest();

            if (new OrderBD().Create(customer.Name, order.Cost, order.OrderStatus, order.OrderDate) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(string customerName, int Cost, string OrderStatus, string OrderDate)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var order = new OrderBD().SearchById(Id);

            if (order == null)
                return BadRequest();
            if (new OrderBD().UpdateOrder(Id, customerName, Cost, OrderStatus, OrderDate) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];
            var order = new OrderBD().SearchById(Id);

            if (order == null)
                return BadRequest();
            if (new OrderBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
            
        }
    }
}