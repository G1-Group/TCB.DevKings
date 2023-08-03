using TCBApp.Interface;
using TCBApp.Models;
using Telegram.Bot.Types;

namespace TCBApp.Services;

public class BoardService:IBoardInterface
{


    public async Task CreateBoard(Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (FindClient(update.Message.Chat.Id) is null)
            {
                SendMessage(update, cancellationToken, "Error permission");
                StartBoard(update, cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task GetBoards(Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Client> FindClient(long id)
    {
        throw new NotImplementedException();
    }

    public async Task StartBoard(Update update, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SendMessage(Update update, CancellationToken cancellationToken, string text)
    {
        throw new NotImplementedException();
    }  
}
   
