using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var categories = new CategoryBD().Read();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var category = new CategoryBD().SearchById(Id);
            if (category == null) 
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("add_category")]
        public IActionResult Post([FromBody] Category category)
        {
            if (category.Title == null)
            {
                return BadRequest();
            }

            var callback = new CategoryBD().Create(category.Title);

            if (callback != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put(string Title)
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var category = new CategoryBD().SearchById(Id);

            if (category == null)
                return BadRequest();

            if (new CategoryBD().UpdateTitle(Id, Title) != -1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var category = new CategoryBD().SearchById(Id);

            if (category == null)
                return BadRequest();

            if (new CategoryBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
        }
    }
}