using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using System.Net;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Stockroom")]
    [ApiController]
    public class StockroomController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var stockrooms = new StockroomBD().Read();
            return Ok(stockrooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var stockroom = new StockroomBD().SearchById(Id);
            if (stockroom == null)
                return NotFound();
            return Ok(stockroom);
        }
        
        [HttpGet("find")]
        public IActionResult GetStockroom([FromBody] Product product)
        {
            var stockroom = new StockroomBD().SearchByProduct(product.Id);
            if (stockroom == null)
                return NotFound();
            return Ok(stockroom);
        }

        [HttpPost("add_stockroom")]
        public IActionResult Post(int productId, string Address, int Amount)
        {
            if (productId == null || Address == null || Amount == null) return BadRequest();
            if (new StockroomBD().Create(productId, Address, Amount) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(int productId, string Address, int Amount)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());

            if (new StockroomBD().SearchById(Id) == null)
                return BadRequest();

            if (new StockroomBD().UpdateAmount(Id, productId, Address, Amount) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];

            if (new StockroomBD().SearchById(Id) == null)
                return BadRequest();

            if (new ProductBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
            
        }
    }
}