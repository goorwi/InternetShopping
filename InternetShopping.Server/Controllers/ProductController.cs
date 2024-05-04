using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using ShopLibrary;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var products = new ProductBD().Read();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            
            var product = new ProductBD().SearchById(Id);

            if (product == null)
                return BadRequest();
            else 
                return Ok(product);
        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] Product product)
        {
            if (product.Title == null || product.Producer == null || product.Price < 0)
                return BadRequest();

            if (new ProductBD().Create(product.Title, new CategoryBD().SearchById(product.CategoryId).Title, product.Producer, new SupplierBD().SearchById(product.SupplierId).Title, product.Content, product.Price) == -1)
                return BadRequest();
            else 
                return Ok();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put([FromBody] Product product)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());

            if (new ProductBD().SearchById(Id) == null) return BadRequest();

            if (new ProductBD().UpdateProduct(Id, product.Title, new CategoryBD().SearchById(product.CategoryId).Title, product.Producer, new SupplierBD().SearchById(product.SupplierId).Title, product.Content, product.Price) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            if (new ProductBD().SearchById(Id) == null) return BadRequest();

            if (new ProductBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
        }
    }
}