namespace TCBApp.Services;

public interface IConversationService
{
    int CreateConversation(ChatModel chat);
    Task StopConversation(long chatId);
}