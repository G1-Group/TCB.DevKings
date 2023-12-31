using TCBApp.Models;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot.Types.ReplyMarkups;

namespace TCBApp.TelegramBot.Extensions;

public static class KeyboardButtonExtensions
{
    public static ReplyKeyboardMarkup MakeClientDashboardReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton("User Info"),
                new KeyboardButton("Settings⚙️"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Boards"),
                new KeyboardButton("Conversations"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Log out")
            }
        };

        return MakeDefaultReplyKeyboardMarkup(buttons);
    }

    public static ReplyKeyboardMarkup FindBoardNickNameReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<KeyboardButton>()
        {
            new KeyboardButton("Send message"),
            new KeyboardButton("Messages"),
            new KeyboardButton("Back"),
        };

        return MakeDefaultReplyKeyboardMarkup(buttons.ToArray());
    }

    public static ReplyKeyboardMarkup MakeBoardsReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton("My boards"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Create"),
                new KeyboardButton("Find board"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Get board messages"),
                new KeyboardButton("Back"),
            }
        };
        return MakeDefaultReplyKeyboardMarkup(buttons);
    }

    private static ReplyKeyboardMarkup MakeDefaultReplyKeyboardMarkup(List<List<KeyboardButton>> buttons)
    {
        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };
    }

    public static ReplyKeyboardMarkup MakeDefaultReplyKeyboardMarkup(params KeyboardButton[] buttons)
    {
        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };
    }

    public static ReplyKeyboardMarkup RequesPhoheNumberReplyKeyboardMarkup(this UserControllerContext contexg)
        => MakeDefaultReplyKeyboardMarkup(new KeyboardButton("Send my phone number") { RequestContact = true });

    public static ReplyKeyboardMarkup SettingsStartReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton("Delete Accaunt"),
                new KeyboardButton("Delete Boards")
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Back")
            }
        };

        return MakeDefaultReplyKeyboardMarkup(buttons);
    }

    public static ReplyKeyboardMarkup ConversationStartReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton("start"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("history"),
                new KeyboardButton("back")
            }
        };

        return MakeDefaultReplyKeyboardMarkup(buttons);
    }

    public static InlineKeyboardMarkup MyBoardsKeyboardMarkup(this UserControllerContext context,List<BoardModel> boards)
    {
        IEnumerable<InlineKeyboardButton> buttons = boards
            .Select(board =>
                new InlineKeyboardButton(board.NickName)
                {
                    CallbackData = board.Id.ToString(),
                });

        return new InlineKeyboardMarkup(buttons);
    }
    
    public static InlineKeyboardMarkup FindBoardsReplyKeyboardMarkup(this UserControllerContext context)
    {
        var inlineButton = new InlineKeyboardButton("find")
        {
            SwitchInlineQueryCurrentChat = ""
        };

        return new InlineKeyboardMarkup(inlineButton);
    }
    
    public static ReplyKeyboardMarkup Back(this UserControllerContext context)
        => MakeDefaultReplyKeyboardMarkup(new KeyboardButton("Back"));
    public static ReplyKeyboardMarkup StopConversationReplyKeyboardMarkup(this UserControllerContext context)
        => MakeDefaultReplyKeyboardMarkup(new KeyboardButton("stop"));

    public static ReplyKeyboardMarkup MakeClientInfoReplyKeyboardMarkup(this UserControllerContext context)
    {
        var buttons = new List<List<KeyboardButton>>()
        {
            new List<KeyboardButton>()
            {
                new KeyboardButton("Edit nick name"),
                new KeyboardButton("Edit user name"),
            },
            new List<KeyboardButton>()
            {
                new KeyboardButton("Back")
            }
        };

        return MakeDefaultReplyKeyboardMarkup(buttons);
    }
    
}