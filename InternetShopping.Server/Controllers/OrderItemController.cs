using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Order_item")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        [HttpGet("get_all")]
        public IActionResult Get()
        {
            var orderItems = new OrderItemBD().Read();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var orderItem = new OrderItemBD().SearchById(Id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] OrderItem item)
        {
            if (new ProductBD().SearchById(item.ProductId) == null || item.Amount < 0 || new OrderBD().SearchById(item.OrderId) == null || new StockroomBD().SearchById(item.StockroomId) == null)
                return BadRequest();

            if (new OrderItemBD().Create(item.ProductId, item.Amount, item.OrderId, item.StockroomId) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int productId, int Amount, int orderId, int stockroomId)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            if (new ProductBD().SearchById(productId) == null || Amount < 0 || new OrderBD().SearchById(orderId) == null || new StockroomBD().SearchById(stockroomId) == null)
                return BadRequest();

            if (new OrderItemBD().SearchById(Id) == null)
                return BadRequest();

            if (new OrderItemBD().Update(Id, productId, Amount, orderId, stockroomId) != -1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];
            if (new OrderItemBD().SearchById(Id) == null)
                return BadRequest();

            if (new OrderItemBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
        }
    }
}