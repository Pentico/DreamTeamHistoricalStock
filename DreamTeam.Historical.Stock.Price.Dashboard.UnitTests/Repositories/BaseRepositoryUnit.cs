using System;
using DreamTeam.Historical.Stock.Price.Dashboard.Data.Factory;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DreamTeam.Historical.Stock.Price.Dashboard.UnitTests.Repositories;

public abstract class BaseRepositoryUnit
{
    protected readonly IDatabaseConnectionFactory ConnectionFactory;

    protected BaseRepositoryUnit()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        var configSettings = new ConfigSettings() { ConnectionStringDatabase = config["ConnectionString"], SourceId = Guid.Parse(config["SourceId"]) };
        var options = Options.Create(configSettings);

        ConnectionFactory = new PostgresConnectionFactory(options);
    }
}