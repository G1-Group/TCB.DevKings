using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services.DataService;

public interface IUserDataService : IDataServiceBase<User>
{
    void Do();
}