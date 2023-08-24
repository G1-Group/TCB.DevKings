using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using TCBApp.Services.DataContexts;

namespace TCBApp.Services.DataService;

public class BoardDataService : DataServiceBase<BoardModel>
{
    public BoardDataService(DataContext context) : base(context)
    {
    }
}