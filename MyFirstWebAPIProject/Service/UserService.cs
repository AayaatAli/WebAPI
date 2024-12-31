using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Service
{
    public class UserService: IUserService
    {
        private IConfiguration Configuration;

        public UserService(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public UserModel AuthenticateUser(UserModel login_user)
        {
            UserModel user = null;
            if (login_user.UserName == "Aayaat" && login_user.Password == "@@y@@t")
            {
                user = new UserModel
                {
                    UserName = login_user.UserName,
                    Password = login_user.Password,
                };
                return user;
            }
            else
            {

                return user;
            }

        }

        public string GenerateToken(UserModel login_user)
         {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,login_user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"],claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
