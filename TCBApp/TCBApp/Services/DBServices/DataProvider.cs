using Npgsql;
using TCBApp.Models;

namespace TCBApp.Services;

public class DataProvider
{

    private readonly string _connectionString;

    public DataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public NpgsqlConnection CreateNewConnection()
    {
        return new NpgsqlConnection(this._connectionString);
    }

    public async Task<NpgsqlDataReader>ExecuteWithResult(string query, NpgsqlParameter[]? parameters)
    {
        var connection = this.CreateNewConnection();
        await connection.OpenAsync();

        var command = new NpgsqlCommand(query, connection);
        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        var paramsStr = string.Join(", ", command.Parameters.Select(x => $"{x.ParameterName} = {x.Value ?? "[NULL]"}"));
        Console.WriteLine("{1} | DB Query: {0}", command.CommandText + "\n" + paramsStr, DateTime.Now);

        var reader = await command.ExecuteReaderAsync();

        return reader;
    }
    
    public async Task<int> ExecuteNonResult(string query, NpgsqlParameter[]? parameters)
    {
        var connection = this.CreateNewConnection();
        await connection.OpenAsync();

        var command = new NpgsqlCommand(query, connection);
        if (parameters is not null)
            command.Parameters.AddRange(parameters);

        var result = await command.ExecuteNonQueryAsync();

        return result;
    }
    
}
   


