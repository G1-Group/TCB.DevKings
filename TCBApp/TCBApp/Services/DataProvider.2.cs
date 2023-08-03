namespace TCBApp.Services;

public partial class DataProvider
{
    private List<ChatModel> Chats { get; set; }

    
    public ChatModel CreateConversation(ChatModel chat)
    {
        bool isExisting = CheckExistConversation(chat.Id);
        if (isExisting)
        {
            Chats.Add(chat );
            return chat;
        }

        throw new Exception("Chat all ready exist");
    }
    
    public ChatModel StopConversation(long chatId)
    {
        var chat = GetConversation(chatId);

        if (chat is not null)
        {
            Chats.Remove(chat);
            return chat;
        }

        throw new Exception("Chat is not found");
    }

    public List<ChatModel> GetAllConversation()
    {
        return Chats;
    }

    public ChatModel UpdateConversation(ChatModel chat)
    {
       Chats.Remove( Chats.Find(item => item.Id == chat.Id));
        Chats.Add(chat);
        return chat;
    }

    public ChatModel GetConversation(long chatId)
    {
        return Chats.Find(chat => chat.Id == chatId);
    }
    
    private bool CheckExistConversation(long chatId)
    {
        var res=Chats.Find(item => item.Id != chatId);
        if (res is not null)
        {
            return true;
        }
        return false;
    }


  
}