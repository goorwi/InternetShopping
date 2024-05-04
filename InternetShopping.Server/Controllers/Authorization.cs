using InternetShopping.Server.funcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ShopLibrary;
using System.Security.Cryptography;
using System.Text;
using TestShop;

namespace InternetShop.Controllers
{
    [Route("Authorization")]
    [ApiController]
    public class Authorization : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = new CustomerBD().SearchByEmail(request.Email);

            if (user == null || !VerifyPassword(request.Password, user.Password))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            string token = AuthorizationFunc.GenerateJwtToken(user);

            return Ok(new { user, Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Customer customer)
        {
            var user = new CustomerBD().SearchByEmail(customer.Email);

            if (user != null)
                BadRequest(new { message = "User with like this email is alreay exists" });

            var passwordHash = HashPassword(customer.Password);
            customer.Password = passwordHash;

            var allUsers = new CustomerBD().Read();

            if (allUsers == null)
                new CustomerBD().Create(customer.Name, customer.Address, customer.Phone, customer.Email, customer.Password, true);
            else
                new CustomerBD().Create(customer.Name, customer.Address, customer.Phone, customer.Email, customer.Password, false);

            string token = AuthorizationFunc.GenerateJwtToken(customer);

            return Ok(new { customer, Token = token });
        }

        [HttpPost("logout")]
        public IActionResult LogOut()
        {
            // Очищаем токен из Local Storage
            Response.Cookies.Delete("token");

            // Отправляем ответ об успешном выходе из системы
            return Ok(new { message = "User logged out successfully" });
        }
        // Method to hash the password
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}