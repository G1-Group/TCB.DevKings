using TCBApp.Services;

namespace TCBApp.Models;

public record SendMessageToConversationViewModel
(
     long FromId ,
     long ToId ,
     long ConversationId,
     Telegram.Bot.Types.Message Content
);