using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services.Interface;

public interface IUserDataService : IDataServiceBase<User>
{
    void Do();
}