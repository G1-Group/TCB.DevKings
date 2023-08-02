namespace TCBApp;

public class Client
{
    public long ClientId { get; set; }
    public long  UserId { get; set; }
    public long  TelegramClientId { get; set; }
    public string Nickname { get; set; }
    public ClientStatus Status { get; set; }
    public bool IsPremium { get; set; }
}