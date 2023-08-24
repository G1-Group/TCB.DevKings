using TCBApp.Models;

namespace TCBApp.Services;

public interface IAuthService
{
    Task RegisterUser(UserRegistrationModel userRegistration);
    Task<Client?> Login(UserLoginModel user);
}