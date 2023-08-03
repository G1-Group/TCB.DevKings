using TCBApp.Models;

namespace TCBApp.Interface;

public interface IConversationInterface
{
  public List<Client> WaitingUsers { get; set; }
  public ChatModel GetLastConversation(long ClientId);

  public int CheckConversationLimit(long ClientId);

  public ChatModel CreateNewConversation(long fromClient, long toClient);
  public ChatModel StopConversation(long chatId);

  public void ExchangeMessage(long fromId, long toId, string message);

  public void DeleteConversation(long chatId);

  public List<ChatModel> GetAllConversation();
}