namespace TCBApp.Models;

public record SendMessageToBoardViewModel(
    long FromId,
    Telegram.Bot.Types.Message Content,
    long BoardId
    );