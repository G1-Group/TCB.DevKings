using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{
    public User CreateUser(long user_Id, string phoneNumber, long telegramClientId, string password)
    {
        throw new NotImplementedException();
    }

    public bool RemoveUser(long user_Id)
    {
        throw new NotImplementedException();
    }

    public User Finduser(long user_Id)
    {
        throw new NotImplementedException();
    }
}