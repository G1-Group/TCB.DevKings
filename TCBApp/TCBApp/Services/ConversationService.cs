using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ConversationService:IConversationInterface
{
    public List<Client> VaitingUsers { get; set; }

    public ChatModel GetLastConversation(long ClientId)
    {
        throw new NotImplementedException();
    }

    public int CheckConversationLimit(long ClientId)
    {
        throw new NotImplementedException();
    }

    public ChatModel CreateNewConversation(long fromClient, long toClient)
    {
        throw new NotImplementedException();
    }

    public ChatModel StopConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public void ExchangeMessage(long fromId, long toId, string message)
    {
        throw new NotImplementedException();
    }

    public void DeleteConversation(long chatId)
    {
        throw new NotImplementedException();
    }

    public List<ChatModel> GetAllConversation()
    {
        throw new NotImplementedException();
    }
}