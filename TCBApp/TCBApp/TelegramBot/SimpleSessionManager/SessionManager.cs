using TCBApp.Models;
using TCBApp.Services;

namespace TCBApp.TelegramBot.Managers;

public class SessionManager : ISessionManager<Session>
{
    private readonly UserDataService _userDataService;
    private List<Session> sessions => new List<Session>();

    public SessionManager(UserDataService userDataService)
    {
        _userDataService = userDataService;
    }

    public async Task<Session> GetSessionByChatId(long chatId)
    {
        var user = await _userDataService.GetUserByChatId(chatId);
        if (user is null)
            throw new Exception("User not found");

        return new Session()
        {
            User = user,
            Action = null,
            Controller = null,
            Id = 0
        };
    }


}