namespace TCBApp.Interface;

public interface IConversationInterface
{
    public int CreateConversation(ChatModel chat);
    public ChatModel StopConversation(long chatId);
    public ChatModel  GetLastConversation(long chatId);

    public ChatModel UpdateConversation(ChatModel chat);
    public ChatModel GetConversation(long chatId);

    public List<ChatModel> GetAllConversation();
    
    


}