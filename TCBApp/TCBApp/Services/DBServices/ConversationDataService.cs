using System.Data;
using Npgsql;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;


public class ConversationDataService : DataProvider
{
    private static string tableName = "anonym_chats";

    private string selectQuery = $"SELECT * FROM {tableName}";
    private string selectByIdQuery = $"SELECT * FROM {tableName} WHERE chat_id = @p0";

    private string insertQuery =
        $"INSERT INTO {tableName} (chat_id,state,created_date,from_id,to_id) VALUES (@p0, @p1, @p2, @p3,@p4);";
    
    public ConversationDataService(string connectionString) : base(connectionString)
    {
    }

    public async Task<List<ChatModel>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<ChatModel> result = new List<ChatModel>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<ChatModel?> GetById(int id)
    {
        var reader = await this.ExecuteWithResult(this.selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<ChatModel> result = new List<ChatModel>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    public async Task<int> Insert(ChatModel chat)
    {
        return await this.ExecuteNonResult(this.insertQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0",chat.Id ),
            new NpgsqlParameter("@p1", chat.State),
            new NpgsqlParameter("@p2", chat.CreatedDate),
            new NpgsqlParameter("@p3", chat.FromId),
            new NpgsqlParameter("@p4", chat.ToId),
        });
    }

    private ChatModel ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new ChatModel()
        {
            Id = reader.GetInt32(0),
            State =  (ChatState)Enum.Parse(typeof(ChatState), reader.GetString(1), true),
            CreatedDate = reader.GetDateTime(2),
            FromId = reader.GetInt32(3),
            ToId  = reader.GetInt32(4), 
        };
    }

}