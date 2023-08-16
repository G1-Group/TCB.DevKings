namespace TCBApp.Models;

public class Session
{
    public long Id { get; set; }
    public User User { get; set; }
    public string Action { get; set; }
    public string Controller { get; set; }
    public long ChatId { get; set; }
    public long? ClientId { get; set; }
    public long? ActiveConversationId { get; set; }
    public UserRegistrationModel RegistrationModel { get; set; }
    public BoardSessionModel BoardData{ get; set; }
    public LoginSessionModel LoginData { get; set; }
}