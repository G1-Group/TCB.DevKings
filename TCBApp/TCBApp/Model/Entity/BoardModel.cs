using System.ComponentModel;
using TCBApp.Models.Enums;

namespace TCBApp.Models;

public class BoardModel
{
    [Description("board_id")]
    public long BoardId { get; set; }
    
    [Description("nickname")]
    public string NickName { get; set; }
    
    [Description("owner_id")]
    public long OwnerId { get; set; }
    
    [Description("board_status")]
    public BoardStatus BoardStatus { get; set; }
}