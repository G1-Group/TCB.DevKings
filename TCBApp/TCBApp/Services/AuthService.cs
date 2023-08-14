using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services.DataContexts;

namespace TCBApp.Services;

public class AuthService:IAuthInterface
{
    private UserDataService _userDataService;
    private ClientDataService _clientDataService;
    private DataContext DataContext;

    public AuthService(UserDataService userDataService, ClientDataService clientDataService,DataContext dataContext)
    {
        DataContext = dataContext;
        _userDataService = userDataService;
        _clientDataService = clientDataService;
    }

    
    public async Task RegisterUser(UserRegistrationModel userRegistration)
    {
        var insertedUser =  DataContext.users.Add(new User()
        {
            Password = userRegistration.Password,
            PhoneNumber = userRegistration.PhoneNumber,
            TelegramClientId = userRegistration.ChatId
        }).Entity;
        DataContext.SaveChanges();
        if (insertedUser is null)
            Console.WriteLine("Unable to insert user");

        if (insertedUser != null)
        {
            var client = new Client()
            {
                UserId = insertedUser.UserId, // Updated line
                Status = ClientStatus.Enabled,
                Nickname = string.Empty,
                UserName = string.Empty,
                IsPremium = false,
            };
            DataContext.clients.Add(client);
        }

        DataContext.SaveChanges();

    }


    public async Task<Client?> Login(UserLoginModel user)
    {
        var userInfo =  DataContext.users.FirstOrDefault(item => item.Password==user.Password&&item.PhoneNumber==user.Login);

        if (userInfo is not null)
        {
            return await _clientDataService.GetByUserId(userInfo.UserId);
        }

        DataContext.SaveChanges();
        return null;
    }
}