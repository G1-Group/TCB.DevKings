using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{
    private UserDataService _userDataService;
    private ClientDataService _clientDataService;

    public AuthService(UserDataService userDataService, ClientDataService clientDataService)
    {
        _userDataService = userDataService;
        _clientDataService = clientDataService;
    }

    
    public async Task RegisterUser(UserRegistrationModel userRegistration)
    {
        var insertedUser = await _userDataService.Insert(new User()
        {
            Password = userRegistration.Password,
            PhoneNumber = userRegistration.PhoneNumber,
            TelegramClientId = userRegistration.ChatId
        });
        if (insertedUser is null)
            throw new Exception("Unable to insert user");
        await _clientDataService.Insert(new Client()
        {
            UserId = insertedUser.UserId,
            Status = ClientStatus.Enabled,
            Nickname = string.Empty,
            UserName = string.Empty,
            IsPremium = false,
        });
    }


    public async Task<Client> Login(UserLoginModel user)
    {
        var userInfo = await _userDataService.GetUserByLoginAndPassword(user.Login, user.Password);

        if (userInfo is not null)
        {
            return await _clientDataService.GetByUserId(userInfo.UserId);
        }
        return null;
    }
}