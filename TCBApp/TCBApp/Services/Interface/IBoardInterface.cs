using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Interface;

public interface IBoardInterface
{
    public Task CreateBoard(Update update, CancellationToken cancellationToken);
    public Task GetBoards(Update update, CancellationToken cancellationToken);
    public  Task<Client> FindClient(long id);
    public Task StartBoard(Update update,CancellationToken cancellationToken);
    public Task SendMessage(Update update, CancellationToken cancellationToken, string text);
}