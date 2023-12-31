using TCBApp.Models;
using TCBApp.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TCBApp.TelegramBot.Controllers;

public class ClientController:ControllerBase
{
    private ClientService _clientService;
    private string userName;
    private string nickName;
    public ClientController(ITelegramBotClient botClient,ClientService clientService, ControllerManager.ControllerManager controllerManager) : base(controllerManager)
    {
        _clientService = clientService;
    }

    
    public void ClientUpdateUserName(UserControllerContext context)
    {
        userName = context.Update.Message.Text;
        // _clientService.UpdateClientUserName(new Client()
        // {   
        //     
        //     // ClientId = context.Update.Message.Chat.Id,
        //     UserName = userName
        // });
        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Succesfuly changed");
        context.Session.Action = "UpdateClientUsername";

    }

    public void ClientUpdateNickName(UserControllerContext context)
    {
        nickName = context.Update.Message.Text;
        // _clientService.UpdateClientNickName(new Client()
        // {
        //     // ClientId = context.Update.Message.Chat.Id,
        //     Nickname = nickName
        // });
        _botClient.SendTextMessageAsync(context.Update.Message.Chat.Id, "Succesfuly changed");
        context.Session.Action = "UpdateClientNickname";

    }
    protected override async Task HandleAction(UserControllerContext context)
    {
        if (context.Session.Action==nameof(ClientUpdateUserName))
        {
            this.ClientUpdateUserName(context);
        }
        else  if(context.Session.Action==nameof(this.ClientUpdateNickName))
        {
            this.ClientUpdateNickName(context);
        }
        
        
    }

    protected override Task HandleUpdate(UserControllerContext context)
    {
        throw new NotImplementedException();
    }
}