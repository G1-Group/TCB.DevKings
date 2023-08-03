using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ClientService:DataProvider, IClientService
{
    public Client CreateClient(Client data)
    {
        base.SaveClient(data);
        return data;
    }

    public Client RemoveClient(Client data)
    {
        base.RemoveClient(data);
        return data;
    }

    public Client FindClient(Client data)
    {
        return base.FindClient(data);
    }

    public Client UpdateClient(Client data)
    {
        throw new NotImplementedException();
    }
}