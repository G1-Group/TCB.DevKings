using TCBApp.Models;

namespace TCBApp.Services;

public interface IClientService
{
    Task<Client> UpdateClientUserName(long clientId, string newUsername);
    Task<Client> UpdateClientNickName(long clientId, string newNickname);
}