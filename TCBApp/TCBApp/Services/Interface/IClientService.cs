using TCBApp.Models;

namespace TCBApp.Interface;

public interface IClientService
{
    public Task<Client> CreateClient(Client data);
    public Task<Client> RemoveClient(Client data);
    public Task<Client> FindClient(Client data);
    public Task<Client> UpdateClient(Client data);
}