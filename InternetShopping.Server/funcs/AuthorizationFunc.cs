using Microsoft.IdentityModel.Tokens;
using ShopLibrary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternetShopping.Server.funcs
{
    public class AuthorizationFunc
    {
        public static string GenerateJwtToken(Customer user)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("KeyStrings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.IsAdmin ? "admin" : "user"),
                    new Claim("nickname", user.Name),
                    new Claim(ClaimTypes.Name, user.Name != null ? user.Name.ToString() : ""),
                    new Claim("addressId", user.Address !=null ?user.Address.ToString() : "" )
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
