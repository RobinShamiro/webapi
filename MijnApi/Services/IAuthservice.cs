using MijnApi.Models;

namespace MijnApi.Services
{
    public interface IAuthService
    {
        AuthResult Register(User user);
        AuthResult Login(User user);
    }
}