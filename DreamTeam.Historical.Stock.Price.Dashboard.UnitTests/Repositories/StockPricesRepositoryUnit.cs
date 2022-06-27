using System;
using System.Linq;
using DreamTeam.Historical.Stock.Price.Dashboard.Data.Repositories;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IRepository;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.FilterCriterias;
using FluentAssertions;
using Xunit;

namespace DreamTeam.Historical.Stock.Price.Dashboard.UnitTests.Repositories;


[Collection("TestingDatabaseCollection")]
public class StockPricesRepositoryUnit: BaseRepositoryUnit
{
    private readonly IStockPricesRepository _stockPriceRepository;

    public StockPricesRepositoryUnit()
    {
        _stockPriceRepository = new StockPricesRepository(ConnectionFactory);
    }

    [Theory]
    [InlineData("CBB15B52-820F-461D-B9FD-EAA5F2C6349B", "TestingInformatino", "TestingSymbol", "TestingOutputSize", "testingTimezone", "testingTimeSeries")]
    public void InsertEntry(Guid id, string information, string symbol, string outputSize, string timeZone, string timeSeries)
    {
        var entry = new StockPriceModel
        {
            Id = id,
            Information = information,
            Symbol = symbol,
            DateCreated = DateTime.UtcNow,
            OutputSize = outputSize,
            TimeSeries = timeSeries,
            TimeZone = timeZone,
            LastRefreshed = DateTime.UtcNow
        };
        
        
        this._stockPriceRepository.InsertStockPrice(entry);
        var dbEntryList = this._stockPriceRepository.GetAllStockPricesList(new StockPricesFilterCriteria()
        {
            StockPriceKeys = new Guid[] { entry.Id }
        });

        var entryModels = dbEntryList as StockPriceModel[] ?? dbEntryList.ToArray();
        entryModels.Should().ContainSingle(x => x.Id == entry.Id);
        var dbEntry = entryModels.Single(x => x.Id == entry.Id);
        dbEntry.Information.Should().Be(entry.Information);
        dbEntry.Symbol.Should().Be(entry.Symbol);
        
        dbEntry.OutputSize.Should().Be(entry.OutputSize);
        dbEntry.TimeSeries.Should().Be(entry.TimeSeries);
        
        dbEntry.TimeZone.Should().Be(entry.TimeZone);
    }
    
    [Theory]
    [InlineData("854F1766-B8C0-4980-B50C-F7A15BCC16BC", "TestingInformatino", "TestingSymbol", "TestingOutputSize", "testingTimezone", "testingTimeSeries")]
    public void EntryFilterCriteriaByCartKeys(Guid id, string information, string symbol, string outputSize, string timeZone, string timeSeries)
    {
        var entry = new StockPriceModel
        {
            Id = id,
            Information = information,
            Symbol = symbol,
            DateCreated = DateTime.UtcNow,
            OutputSize = outputSize,
            TimeSeries = timeSeries,
            TimeZone = timeZone,
            LastRefreshed = DateTime.UtcNow
        };

        this._stockPriceRepository.InsertStockPrice(entry);

        var results = this._stockPriceRepository.GetAllStockPricesList(new StockPricesFilterCriteria()
        {
            StockPriceKeys = new Guid[] { entry.Id }
        });

        results.Count().Should().Be(1);
        results.First().Id.Should().Be(id);
    }
}