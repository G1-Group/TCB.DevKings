using Npgsql;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class MessageDataService:DataProvider
{
    public MessageDataService(string connectionString) : base(connectionString)
    {
    }
    
     private static string tableName = "messages";

    private string selectQuery = $"SELECT * FROM {tableName}";
    
    private string selectByIdQuery = $"SELECT * FROM {tableName} WHERE id = @p0";
    
    string updateQuery =
        $"UPDATE boards SET from_id = @p1 , message = @p2,chat_id=@p3,board_id=@p4,message_type=@p6,message_status=@p5  WHERE id = @p0 ;";
    
    private string insertQuery =
        $"INSERT INTO {tableName} (from_id,message,chat_id,board_id,message_status,message_type) VALUES ( @p1, @p2,@p3,@p4,@p5,@p6);";
    
    string deleteQuery = $"UPDATE messages SET message_status = @p5  WHERE board_id = @p0 ;";
    
    


    public async Task<List<Message>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<Message> result = new List<Message>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<Message?> GetById(long id)
    {
        var reader = await this.ExecuteWithResult(this.selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<Message> result = new List<Message>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    public async Task<int> Insert(Message message) {
        return await this.ExecuteNonResult(this.insertQuery, new NpgsqlParameter[] {
            new NpgsqlParameter("@p1", message.FromId),
            new NpgsqlParameter("@p2", message._Message),
            new NpgsqlParameter("@p3", message.ChatId),
            new NpgsqlParameter("@p4", message.BoardId),
            new NpgsqlParameter("@p5",(int)message.MessageType ),
            new NpgsqlParameter("@p6", (int)message.MessageStatus)
        });
    }



    private Message ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new Message
        {
            FromId = reader.GetInt32(1),
            _Message = reader.GetString(2),
            ChatId = reader.GetInt32(3),
            BoardId = reader.GetInt32(4),
            MessageType = (Models.Enums.MessageType)Enum.Parse(typeof(Models.Enums.MessageType), reader.GetString(5), true),
            MessageStatus = (MessageStatus)Enum.Parse(typeof(MessageStatus), reader.GetString(6), true),
        };
    }
    public async Task<Message> UpdateMessage(long Id, Message message)
    {
        var result= await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p1", message.FromId),
            new NpgsqlParameter("@p2", message._Message),
            new NpgsqlParameter("@p3", message.ChatId),
            new NpgsqlParameter("@p4", message.BoardId),
            new NpgsqlParameter("@p6", message.MessageType),
            new NpgsqlParameter("@p5", message.MessageStatus)
        });
        return await FindByIdMessage(Id);
    }
    
    public async Task<Message> FindByIdMessage(long Id)
    {
        var reader = await this.ExecuteWithResult(selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", Id)
        });
        List<Message> messages = new List<Message>();
        while (reader.Read())
            messages.Add(ReaderDataToModel(reader));
        return messages.FirstOrDefault();

    }
    
    
    public async Task<Message> DeleteMessage(long Id)
    {
        Message message =await FindByIdMessage(Id);
        var result = await ExecuteNonResult(deleteQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", Id)
        });
        return message;
    }
    
}