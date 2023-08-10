using TCBApp.Models;
using TCBApp.Services;
using TCBApp.TelegramBot.Controllers;

namespace TCBApp.TelegramBot.Managers;

public class SessionManager : ISessionManager<Session>
{
    private readonly UserDataService _userDataService;
    private List<Session> sessions = new List<Session>();

    public SessionManager(UserDataService userDataService)
    {
        _userDataService = userDataService;
        
        //TODO: This is only development process
        // sessions.Add(new Session()
        // {
        //     Controller = nameof(ClientDashboardController),
        //     Action = nameof(ClientDashboardController.Index),
        //     ChatId = 1179599037,
        //     ClientId = 8
        // });
    }

    public async Task<Session> GetSessionByChatId(long chatId)
    {
        var lastSession = sessions.Find(x => x.ChatId == chatId);
        if (lastSession is null)
        {
            var session = new Session()
            {
                Action = null,
                Controller = null,
                Id = 0,
                ChatId = chatId
            };
            sessions.Add(session);
            return session;
        }

        return lastSession;
    }

    public async Task TerminateSession(Session session)
    {
        this.sessions.Remove(session);
    }

}