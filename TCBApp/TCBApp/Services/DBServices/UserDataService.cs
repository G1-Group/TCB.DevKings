using System.Data;
using Npgsql;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class UserDataService:DataProvider
{
    
    private static string tableName = "users";

    private string selectQuery = $"SELECT * FROM {tableName}";
    private string selectByIdQuery = $"SELECT * FROM {tableName} WHERE id = @p0";
    private string selectByChatIdQuery = $"SELECT * FROM {tableName} WHERE telegram_client_id= @p0";
    private string selectByLoginAndPassword = $"SELECT * FROM {tableName} WHERE phone_number = @p0 and password = @p1;";
    
    private string insertQuery =
        $"INSERT INTO {tableName} (telegram_client_id,phone_number,password) VALUES ( @p1, @p2, @p3) RETURNING *;";
    
    public UserDataService(string connectionString) : base(connectionString)
    {
    }

    public async Task<List<Models.User>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<Models.User> result = new List<Models.User>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<Models.User?> GetById(long id)
    {
        var reader = await this.ExecuteWithResult(this.selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<Models.User> result = new List<Models.User>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    public async Task<User?> Insert(Models.User user)
    {
        var reader = await this.ExecuteWithResult(this.insertQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p1", user.TelegramClientId),
            new NpgsqlParameter("@p2", user.PhoneNumber),
            new NpgsqlParameter("@p3", user.Password),
        });
        List<Models.User> result = new List<Models.User>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    public async Task<User?> GetUserByChatId(long chatId)
    {
        var reader = await this.ExecuteWithResult(this.selectByChatIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", chatId)
        });
        List<Models.User> result = new List<Models.User>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }
    
    public async Task<User?> GetUserByLoginAndPassword(string login, string password)
    {
        var reader = await this.ExecuteWithResult(this.selectByLoginAndPassword, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", login),
            new NpgsqlParameter("@p1", password)
        });
        List<Models.User> result = new List<Models.User>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    private Models.User ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new Models.User()
        {
            UserId = reader.GetInt64(0),
            TelegramClientId = reader.GetInt64(1),
            PhoneNumber = reader.GetString(2),
            Password = reader.GetString(3)
            
        };
    }


}