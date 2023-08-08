
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
    private string selectByUserIdQuery = $"SELECT * FROM {tableName} WHERE user_id = @p0";

    private string insertQuery =
        $"INSERT INTO {tableName} (user_id,user_name,nickname,status,ispremium) VALUES ( @p1, @p2, @p3,@p4,@p5) RETURNING *;";
    
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
    
    public async Task<Client?> GetByUserId(long id)
    {
        var reader = await this.ExecuteWithResult(this.selectByUserIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<Client> result = new List<Client>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

 
    public async Task<Client?> Insert(Client client)
    {
       var reader = await this.ExecuteWithResult(this.insertQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p1", client.UserId),
            new NpgsqlParameter("@p2", client.UserName),
            new NpgsqlParameter("@p3", client.Nickname),
            new NpgsqlParameter("@p4", (int)client.Status),
            new NpgsqlParameter("@p5", client.IsPremium),
        });
       List<Client> result = new List<Client>();
       while (reader.Read())
           result.Add(this.ReaderDataToModel(reader));

       return result.ElementAtOrDefault(0);
    }
    public async Task<Client?> Update(Client client)
    {
        await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p1", client.UserId),
            new NpgsqlParameter("@p2", client.UserName),
            new NpgsqlParameter("@p3", client.Nickname),
            new NpgsqlParameter("@p4",(int)client.Status),
            new NpgsqlParameter("@p5", client.IsPremium),
        });
        return client; 
    }
    private Client ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new Client()
        {
            ClientId = reader.GetInt64(0),
            UserId = reader.GetInt32(1),
            UserName = reader.GetString(2),
            Nickname = reader.GetString(3),
            Status = (ClientStatus)reader.GetInt32(4),
            IsPremium = reader.GetBoolean(5)
        };
    }
    
    public async Task<int> UpdateUserName(Client client)
    {
        return await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", client.ClientId),
            new NpgsqlParameter("@p2", client.UserName)
        });
        
    }

    public async Task<int> UpdateNickName(Client client)
    {
        return await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", client.ClientId),
            new NpgsqlParameter("@p3", client.Nickname)
        });

    }

}