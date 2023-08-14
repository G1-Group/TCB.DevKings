using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using TCBApp.Models.Enums;
namespace TCBApp.Models;
[Table("clients")]
public class Client
{
    [Column("client_id")]
    public long ClientId { get; set; }
    [Column("user_id")]
    public long  UserId { get; set; }
    [Column("user_name")]
    public string  UserName { get; set; }
    [Column("nickname")]
    public string Nickname { get; set; }
    [Column("status")]
    public ClientStatus Status { get; set; }
    [Column("ispremium")]
    public bool IsPremium { get; set; }
}