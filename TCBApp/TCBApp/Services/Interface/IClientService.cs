using TCBApp.Models;

namespace TCBApp.Interface;

public interface IClientService
{
    public Client CreateClient(Client data);
    public Client RemoveClient(Client data);
    public Client FindClient(Client data);
    public Client UpdateClient(Client data);
}