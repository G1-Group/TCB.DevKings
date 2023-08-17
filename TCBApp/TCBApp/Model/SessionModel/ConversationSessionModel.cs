namespace TCBApp.Models;

public class ConversationSessionModel
{
    public long FromId { get; set; }
    public long FromChatId { get; set; }
    
    public long ToId { get; set; }
    public long ToChatId { get; set; }
}