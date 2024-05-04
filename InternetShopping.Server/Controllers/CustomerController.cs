using InternetShopping.Server.funcs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShopLibrary;
using System.Net;
using System.Numerics;
using System.Xml.Linq;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet("get_all")]
        public IActionResult Get()
        {
            var customers = new CustomerBD().Read();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById()
        {
            int Id = int.Parse(Url.ActionContext.RouteData.Values["id"].ToString());
            var customer = new CustomerBD().SearchById(Id);

            if (customer == null)
                return BadRequest();
            else 
                return Ok(customer);
        }

        [HttpPost("add_customer")]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer.Name == null || customer.Address == null || customer.Phone == null || customer.Email == null || customer.Password == null)
                return BadRequest();

            if (new CustomerBD().Create(customer.Name, customer.Address, customer.Phone, customer.Email, customer.Password, false) != -1)
                return Ok();
            else 
                return BadRequest();
        }

        [HttpPut("{id}/update")]
        public IActionResult Put([FromBody] Customer customer)
        {
            var cust = new CustomerBD().SearchById(customer.Id);

            if (cust == null)
                return BadRequest();

            if (new CustomerBD().Update(customer.Id, customer.Name, customer.Address, customer.Phone, customer.Email, customer.Password, false) != -1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPatch("{id}/patch")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Customer> patchUser)
        {
            if (patchUser == null)
            {
                return BadRequest();
            }

            var existingUser = new CustomerBD().SearchById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            patchUser.ApplyTo(existingUser, ModelState);

            if (!TryValidateModel(existingUser))
            {
                return BadRequest(ModelState);
            }

            new CustomerBD().Update(existingUser.Id, existingUser.Name, existingUser.Address, existingUser.Email, existingUser.Phone, existingUser.Password, false); ;
            var token = AuthorizationFunc.GenerateJwtToken(existingUser);
            return Ok(new { Token = token });
        }

        [HttpDelete("{id}/delete")]
        public IActionResult Delete()
        {
            int Id = (int)Url.ActionContext.RouteData.Values["id"];

            var customer = new CustomerBD().SearchById(Id);
            if (customer == null)
                return BadRequest();

            if (new CustomerBD().Delete(Id) != -1)
                return Ok();
            else
                return BadRequest();
            
        }
    }
}