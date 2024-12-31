using MyFirstWebAPIProject.Models;

namespace MyFirstWebAPIProject.Service
{
    public interface IUserService
    {
        UserModel AuthenticateUser(UserModel login_user);
        string GenerateToken(UserModel login_user);
    }
}
