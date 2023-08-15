using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Services.DataContexts;

namespace TCBApp.Services.DataService;

public class UserDataService : DataServiceBase<User>
{
    public UserDataService(DataContext context) : base(context)
    {
    }
}