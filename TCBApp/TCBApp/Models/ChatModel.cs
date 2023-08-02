namespace TCBApp;

public class ChatModel
{
    public DateTime CreatedDate { get; set; }
    public long FromId { get; set; }
    public long ToId { get; set; }
    public ChatState State { get; set; }
    public long Id { get; set; }
}