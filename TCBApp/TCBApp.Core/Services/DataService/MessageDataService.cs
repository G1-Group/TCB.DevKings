using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Services.DataContexts;

namespace TCBApp.Services.DataService;

public class MessageDataService : DataServiceBase<Message>
{
    public MessageDataService(DataContext context) : base(context)
    {
    }

 
}