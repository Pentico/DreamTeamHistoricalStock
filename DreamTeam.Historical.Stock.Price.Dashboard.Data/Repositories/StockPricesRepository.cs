using Dapper;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IRepository;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.FilterCriterias;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Data.Repositories;

public class StockPricesRepository: RepositoryBase<StockPriceModel, StockPricesFilterCriteria>, IStockPricesRepository
{
    private const string TABLE_NAME = "stockprices";
    
    public StockPricesRepository(IDatabaseConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public override void ApplyFilter(SqlBuilder builder, StockPricesFilterCriteria filterCriteria)
    {
        if (filterCriteria?.StockPriceKeys != null && filterCriteria.StockPriceKeys.Length > 0)
        {
            builder.Where($"Q.id = ANY(@StockPriceKeys)", new {filterCriteria.StockPriceKeys});
        }
    }

    public IList<StockPriceModel> GetAllStockPricesList(StockPricesFilterCriteria filterCriteria)
    {
        var listStockPrice = 
        using var connection = ConnectionFactory.GetConnection();
        var builder = new SqlBuilder();
        var sql = $@"SELECT * FROM {TABLE_NAME} Q /**where**/ ";
        var template = builder.AddTemplate(sql);
                    
        ApplyFilter(builder, filterCriteria);
                    
        var lists = connection.Query<dynamic>(template.RawSql, template.Parameters).ToList();
                    lists.AsParallel().ForAll(item =>
                    {
                        
                    });
        return lists;
    }

    public bool InsertStockPrice(StockPriceModel entry)
    {
        using var connection = ConnectionFactory.GetConnection();

        if (ExistInDb(entry.Id, TABLE_NAME)) return false;
        var sql = $"INSERT INTO {TABLE_NAME}  (id, information, symbol, lastrefreshed, outputsize, timeseries, timezone)" +
                  $"values (@Id, @Information, @Symbol, @LastRefreshed, @OutputSize, @TimeSeries, @TimeZone)";

        dynamic objects = new
        {
            entry.Id,
            entry.MetaData.Information,
            entry.MetaData.Symbol,
            entry.MetaData.LastRefreshed,
            entry.MetaData.OutputSize,
            entry.TimeSeriesDaily,
            entry.MetaData.TimeZone
        };
                    
        var results = this.Insert(sql, objects);
        return results.Equals(1);

    }
    
    
    
}