using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyFirstWebAPIProject.Models;
using MyFirstWebAPIProject.Service;

namespace MyFirstWebAPIProject.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class UserController: ControllerBase
    {

        //it is constructor 
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
            //_studentService = studentService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]UserModel user)
        {
            IActionResult result = Unauthorized();
            UserModel new_user = userService.AuthenticateUser(user);
            if (new_user != null) { 
                var tokenString= userService.GenerateToken(new_user);
                result = Ok(new {token= tokenString});
            }
            return result;
        }
    }
}
