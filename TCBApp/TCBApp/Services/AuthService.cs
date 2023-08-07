using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{
    private UserDataService _userDataService;
    private ClientDataService _clientDataService;

    public AuthService(UserDataService userDataService,ClientDataService clientDataService)
    {
        _userDataService = userDataService;
        _clientDataService = clientDataService;
    }

    
    public async Task Registration(UserRegstration user)
    {
        await _userDataService.Insert(user.User);
        await _clientDataService.Insert(new Client()
        {
            ClientId = user.TelegramChatId,
            UserId = user.User.UserId,
            Status = ClientStatus.Enabled,
            Nickname = null,
            UserName = null,
            IsPremium = false,
        });
    }


    public async Task<Client> Login(UserRegstration user)
    {
        var _user = await _userDataService.GetById(user.User.UserId);
        if (_user.Password == user.User.Password && _user.PhoneNumber == user.User.PhoneNumber)
        {
            return await _clientDataService.GetById(user.TelegramChatId);
        }

        throw new Exception("User not found");
    }
}