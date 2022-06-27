using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Xunit;

namespace DreamTeam.Historical.Stock.Price.Dashboard.UnitTests;

public class TestingDatabaseFixture: IAsyncLifetime
{
    private readonly string _connectionString;

    public TestingDatabaseFixture()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        this._connectionString = config["ConnectionString"];
    }

    public async Task InitializeAsync()
    {
        var con = GetDatabaseConnection();
        await GetCheckpoint().Result.Reset(con);
    }

    public Task DisposeAsync() => Task.CompletedTask;

    private async Task<Checkpoint> GetCheckpoint()
    {
        var checkpoint = new Checkpoint {
            SchemasToInclude = new[]
            {
                "stockprices"
            },
            DbAdapter = DbAdapter.Postgres
        };

        return checkpoint;
    }

    private DbConnection GetDatabaseConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
    
    [CollectionDefinition("TestingDatabaseCollection")]
    public class TestingDatabaseCollection : ICollectionFixture<TestingDatabaseFixture> { }
}