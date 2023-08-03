using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{

    public User Registration(long user_Id, long telgramClientId, string nickName, string password)
    {
        User user = new User()
        {
            UserId = user_Id,
            Password = password,
            Nickname = nickName,
            TelegramClientId = telgramClientId
        };
        return user;
    }

    public Client LogIn(User user)
    {
        return new Client();
    }
}