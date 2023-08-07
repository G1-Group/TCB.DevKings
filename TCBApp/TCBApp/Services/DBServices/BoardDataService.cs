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
    
    string updateQuery = $"UPDATE boards SET owner_id = @p1 , nick_name = @p2  WHERE board_id = @p0 ;";
    
    private string insertQuery =
        $"INSERT INTO {tableName} (nickname,owner_id,board_status) VALUES ( @p1, @p2,@p3);";
    
    string deleteQuery = $"UPDATE boards SET board_status = @p3  WHERE board_id = @p0 ;";
    
    


    public async Task<List<BoardModel>> GetAll()
    {
        var reader = await this.ExecuteWithResult(this.selectQuery, null);
        List<BoardModel> result = new List<BoardModel>();
        while (reader.Read())
            result.Add(this.ReaderDataToModel(reader));

        return result;
    }


    public async Task<BoardModel?> GetById(long id)
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
            new NpgsqlParameter("@p3", (int)board.BoardStatus),
           
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
    public async Task<BoardModel> UpdateBoard(long Id, BoardModel boardModel)
    {
        var result= await this.ExecuteNonResult(updateQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", Id),
            new NpgsqlParameter("@p1", boardModel.OwnerId),
            new NpgsqlParameter("@p2", boardModel.NickName)
        });
        return await FindByIdBoard(Id);
    }
    
    public async Task<BoardModel> FindByIdBoard(long Id)
    {
        var reader = await this.ExecuteWithResult(selectByIdQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", Id)
        });
        List<BoardModel> boards = new List<BoardModel>();
        while (reader.Read())
            boards.Add(ReaderDataToModel(reader));
        return boards.FirstOrDefault();

    }
    
    
    public async Task<BoardModel> DeleteBoard(long Id)
    {
        BoardModel boardModel =await FindByIdBoard(Id);
        var result = await ExecuteNonResult(deleteQuery, new NpgsqlParameter[]
        {
            new NpgsqlParameter("@p0", Id)
        });
        return boardModel;
    }
}