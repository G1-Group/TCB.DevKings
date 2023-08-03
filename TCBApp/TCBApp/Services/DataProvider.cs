using TCBApp.Models;

namespace TCBApp.Services;

public partial class DataProvider
{
    public List<Client> clients = new List<Client>();
    public void SaveClient(Client data)
    {
        clients.Add(data);
    }

    public void RemoveClient(Client data)
    {
       var client= clients.Find(x => x.ClientId == data.ClientId);
       clients.Remove(client);
    }

    public Client FindClient(Client data)
    {
        return clients.Find(x => x.ClientId == data.ClientId);
        
    }
}