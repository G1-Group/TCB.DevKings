using System.ComponentModel;
using TCBApp.Models.Enums;
namespace TCBApp.Models;

public class Client
{
    [Description("client_id")]
    public long ClientId { get; set; }
    [Description("user_id")]
    public long  UserId { get; set; }
    [Description("user_name")]
    public string  UserName { get; set; }
    [Description("nickname")]
    public string Nickname { get; set; }
    [Description("status")]
    public ClientStatus Status { get; set; }
    [Description("ispremium")]
    public bool IsPremium { get; set; }
}