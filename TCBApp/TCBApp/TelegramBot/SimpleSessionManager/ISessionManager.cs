namespace TCBApp.TelegramBot.Managers;

public interface ISessionManager<T>
{
   public Task<T> GetSessionByChatId(long chatId);
}