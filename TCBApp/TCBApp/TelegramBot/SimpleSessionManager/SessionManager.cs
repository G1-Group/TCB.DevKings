using TCBApp.Models;
using TCBApp.Services;

namespace TCBApp.TelegramBot.Managers;

public class SessionManager : ISessionManager<Session>
{
    private readonly UserDataService _userDataService;
    private List<Session> sessions = new List<Session>();

    public SessionManager(UserDataService userDataService)
    {
        _userDataService = userDataService;
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


}