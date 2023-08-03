using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Interface;

public interface IBoardInterface
{
    public Task CreateBoard(string NickName,Update update, CancellationToken cancellationToken);
    public Task GetBoards(Update update, CancellationToken cancellationToken);
}