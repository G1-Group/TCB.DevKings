using Microsoft.EntityFrameworkCore;
using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services.DataContexts;
using TCBApp.Services.DataService;

namespace TCBApp.Services;

public class AuthService : IAuthService
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

        var oldUser  = await this._userDataService.GetAll()
            .FirstOrDefaultAsync(x => x.PhoneNumber == userRegistration.PhoneNumber);

        if (oldUser is User)
            throw new Exception("User already exists");
        
        var insertedUser = await this._userDataService.AddAsync(new User()
        {
            Password = userRegistration.Password,
            PhoneNumber = userRegistration.PhoneNumber,
            TelegramClientId = userRegistration.ChatId
        });
        
        if (insertedUser is null)
            throw new Exception("Unable to insert user");
        var client = new Client()
        {
            UserId = insertedUser.Id, // Updated line
            Status = ClientStatus.Enabled,
            IsPremium = false,
            UserName = string.Empty,
            Nickname = string.Empty
        };

        var insertedClient = await this._clientDataService.AddAsync(client);

        if (insertedClient is null)
            throw new Exception("Unable to add new client");
    }

    public async Task<Client?> Login(UserLoginModel user)
    {
        var userInfo = _userDataService.GetAll()
            .FirstOrDefault(item => 
                item.Password == user.Password
                && item.PhoneNumber == user.Login);

        // if (userInfo is null)
        //     throw new Exception("User not found");
        
        if (userInfo is User)
        {
            userInfo.Signed = true;
            userInfo.LastLoginDate = DateTime.Now;

            await _userDataService.UpdateAsync(userInfo);
            
            return userInfo.Client;
        }

        return null;
    }

    public async Task Logout(long userId)
    {
        var user = await _userDataService.GetByIdAsync(userId);
        if (user is null)
            throw new Exception("User not found");

        user.Signed = false;
        await _userDataService.UpdateAsync(user);
    }
}