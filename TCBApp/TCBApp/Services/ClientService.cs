using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ClientService:DataProvider, IClientService
{
    public Client CreateClient(Client data)
    {
        throw new NotImplementedException();
    }

    public Client RemoveClient(Client data)
    {
        throw new NotImplementedException();
    }

    public Client FindClient(Client data)
    {
        throw new NotImplementedException();
    }

    public Client UpdateClient(Client data)
    {
        throw new NotImplementedException();
    }

    public ClientService(string connectionString) : base(connectionString)
    {
    }
}