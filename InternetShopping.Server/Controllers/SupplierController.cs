using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var suppliers = new SupplierBD().Read();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var supplier = new SupplierBD().SearchById(Id);

            if (supplier == null)
                return BadRequest();
            else
                return Ok(supplier);
        }

        [HttpPost("add_supplier")]
        public IActionResult Post([FromBody] Supplier supplier)
        {
            if (supplier.Title == null)
            {
                return BadRequest();
            }

            var callback = new SupplierBD().Create(supplier.Title);

            if (callback != -1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(string Title)
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];

            if (new SupplierBD().SearchById(Id) == null)
                return BadRequest();

            if (new SupplierBD().UpdateTitle(Id, Title) != -1)
                return Ok();
            else return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];
            var supplier = new SupplierBD().SearchById(Id);

            if (supplier == null)
                return BadRequest();

            if (new SupplierBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
        }
    }
}