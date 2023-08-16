using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Services.DataContexts;

namespace TCBApp.Services.DataService;

public class ClientDataService : DataServiceBase<Client>
{
    public ClientDataService(DataContext context) : base(context)
    {
    }
}