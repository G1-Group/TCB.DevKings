using TCBApp.Models.Enums;

namespace TCBApp.Models;

public class BoardModel
{
    public long BoardId { get; set; }
    public string NickName { get; set; }
    public long OwnerId { get; set; }
    public BoardStatus BoardStatus { get; set; }
}