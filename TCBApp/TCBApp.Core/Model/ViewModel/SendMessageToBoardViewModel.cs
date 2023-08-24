namespace TCBApp.Models;

public record SendMessageToBoardViewModel(
    long FromId,
    object Content,
    long BoardId
    );