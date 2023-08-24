using TCBApp.Services;

namespace TCBApp.Models;

public record SendMessageToConversationViewModel
(
     long FromId ,
     long ToId ,
     long ConversationId,
     object Content
);