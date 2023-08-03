using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{
    private List<User> _users = new List<User>();
    public User Registration(long user_Id, long telgramClientId, string nickName, string password)
    {
        User user = new User()
        {
            UserId = user_Id,
            Password = password,
            Nickname = nickName,
            TelegramClientId = telgramClientId
        };
        _users.Add(user);
        return user;
    }

    public Client LogIn(User user)
    {
       var UserClient = _users.Find(x => x.Nickname == user.Nickname && x.Password == user.Password);
       if (UserClient is not null)
       {
           Client client = new Client()
           {
               ClientId = UserClient.UserId,
               IsPremium = false,
               Nickname = UserClient.Nickname,
               TelegramClientId = user.TelegramClientId,
               Status = ClientStatus.Enabled,

           };
           return client ;
       }

       throw new Exception("User not found");

    }
}