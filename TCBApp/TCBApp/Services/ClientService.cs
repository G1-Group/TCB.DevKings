using TCBApp.Interface;
using TCBApp.Models;

namespace TCBApp.Services;

public class ClientService: IClientService
{
    private ClientDataService _clientDataService;

    public ClientService(ClientDataService clientDataService)
    {
        _clientDataService = clientDataService;
    }

    public async Task<Client?> CreateClient(Client data)
    {
        var client = await _clientDataService.GetById(data.ClientId);
        if (client is null)
        {
           await _clientDataService.Insert(data);
           return data;
        }

        return client;
    }

    public async Task<Client?> RemoveClient(Client data)
    {
       var client=await _clientDataService.Update(data);
       return client;
    }

    public async Task<Client?> FindClient(Client data)
    {
        var client =await _clientDataService.GetById(data.ClientId);
        return client;
    }

    public async Task<Client?> UpdateClient(Client data)
    {
        var client =await _clientDataService.Update(data);
        return client;
    }

    public async Task<int> UpdateClientUserName(Client data)
    {
        var client = await _clientDataService.UpdateUserName(data);
        return client;
    }

    public async Task<int> UpdateClientNickName(Client data)
    {
        var client = await _clientDataService.UpdateNickName(data);
        return client;
    }

}