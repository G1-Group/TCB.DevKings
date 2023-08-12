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

    private static ReplyKeyboardMarkup MakeDefaultReplyKeyboardMarkup(params KeyboardButton[] buttons)
    {
        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };
    }
}