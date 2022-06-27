using System.Data;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.Config;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Data.Factory;

public class PostgresConnectionFactory: IDatabaseConnectionFactory
{
    private readonly string _connectionString;

    public PostgresConnectionFactory(IOptions<ConfigSettings> configSettings)
    {
        _connectionString = configSettings.Value.ConnectionStringDatabase;
    }
    public IDbConnection GetConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}