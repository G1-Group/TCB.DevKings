using TCBApp.Interface;
using TCBApp.Models;
using TCBApp.Services.DataService;

namespace TCBApp.Services;

public class ClientService : IClientService
{
    private ClientDataService _clientDataService;

    public ClientService(ClientDataService clientDataService)
    {
        _clientDataService = clientDataService;
    }
    public async Task<Client> UpdateClientUserName(long clientId, string newUsername)
    {
        var client = await _clientDataService.GetByIdAsync(clientId);
        if (client is null)
            throw new Exception("Client not found!");
        client.UserName = newUsername;
        return await _clientDataService.UpdateAsync(client);
    }

    public async Task<Client> UpdateClientNickName(long clientId, string newNickname)
    {
        var client = await _clientDataService.GetByIdAsync(clientId);
        if (client is null)
            throw new Exception("Client not found!");
        client.Nickname = newNickname;
        return await _clientDataService.UpdateAsync(client);
    }

}