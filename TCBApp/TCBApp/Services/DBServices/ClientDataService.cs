
using Npgsql;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class ClientDataService:DataProvider
{
    private static string tableName = "clients";

    private string updateQuery =
        $"UPDATE {tableName} SET (user_id,user_name,nickname,status,ispremium) VALUES ( @p1, @p2, @p3,@p4,@p5) WHERE client_id=@p0; ";
    private string selectQuery = $"SELECT * FROM {tableName}";
    private string selectByIdQuery = $"SELECT * FROM {tableName} WHERE client_id = @p0";

    private string insertQuery =
        $"INSERT INTO {tableName} (user_id,user_name,nickname,status,ispremium) VALUES ( @p1, @p2, @p3,@p4,@p5);";
    
    public ClientDataService(string connectionString) : base(connectionString)
    {
    }

    public async Task<List<Client>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<Client> result = new List<Client>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<Client?> GetById(long id)
    {
        var reader = await this.ExecuteWithResult(this.selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<Client> result = new List<Client>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

 
    public async Task<int?> Insert(Client client)
    {
       return  await this.ExecuteNonResult(this.insertQuery, new NpgsqlParameter[]
        {
            
            new NpgsqlParameter("@p1", client.UserId),
            new NpgsqlParameter("@p2", client.UserName),
            new NpgsqlParameter("@p3", client.Nickname),
            new NpgsqlParameter("@p4", client.Status),
            new NpgsqlParameter("@p5", client.IsPremium),
        });
      
    }
    public async Task<Client?> Update(Client client)
    {
        await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0",client.ClientId),
            new NpgsqlParameter("@p1", client.UserId),
            new NpgsqlParameter("@p2", client.UserName),
            new NpgsqlParameter("@p3", client.Nickname),
            new NpgsqlParameter("@p4", client.Status),
            new NpgsqlParameter("@p5", client.IsPremium),
        });
        return client; 
    }
    private Client ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new Client()
        {
            ClientId = reader.GetInt32(0),
            UserId = reader.GetInt32(1),
            UserName = reader.GetString(2),
            Nickname = reader.GetString(3),
            Status = (ClientStatus)Enum.Parse(typeof(ClientStatus), reader.GetString(4), true),
            IsPremium = reader.GetBoolean(5)
        };
    }

}