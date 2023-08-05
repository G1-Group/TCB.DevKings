using Npgsql;
using TCBApp.Models;
using TCBApp.Models.Enums;

namespace TCBApp.Services;

public class BoardDataService:DataProvider
{
    
    public BoardDataService(string connectionString) : base(connectionString)
    {
    }
    
    private static string tableName = "boards";

    private string selectQuery = $"SELECT * FROM {tableName}";
    private string selectByIdQuery = $"SELECT * FROM {tableName} WHERE board_Id = @p0";

    private string insertQuery =
        $"INSERT INTO {tableName} (nickname,owner_id,board_status) VALUES ( @p1, @p2,@p3);";
    


    public async Task<List<BoardModel>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<BoardModel> result = new List<BoardModel>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<BoardModel?> GetById(int id)
    {
        var reader = await this.ExecuteWithResult(this.selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", id)
        });
        List<BoardModel> result = new List<BoardModel>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result.ElementAtOrDefault(0);
    }

    public async Task<int> Insert(BoardModel board)
    {
        return await this.ExecuteNonResult(this.insertQuery, new NpgsqlParameter[]
        {
            
            new NpgsqlParameter("@p1", board.NickName),
            new NpgsqlParameter("@p2", board.OwnerId),
            new NpgsqlParameter("@p3", board.BoardStatus),
           
        });
    }

    private BoardModel ReaderDataToModel(NpgsqlDataReader reader)
    {
        return new BoardModel()
        {
            BoardId = reader.GetInt32(0),
            NickName= reader.GetString(1),
            OwnerId = reader.GetInt32(2),
            BoardStatus= (BoardStatus)Enum.Parse(typeof(BoardStatus), reader.GetString(3), true),
           
        };
    }
}