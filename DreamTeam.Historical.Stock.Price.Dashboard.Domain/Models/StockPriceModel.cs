namespace DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models;

public class StockPriceModel: BaseModel
{
    public MetaData MetaData { get; set; }
    public  dynamic TimeSeriesDaily { get; set; }
}

public class MetaData
{
    public string Information { get; set; }
    public  string Symbol { get; set; }
    public  DateTime LastRefreshed { get; set; }
    public  string OutputSize { get; set; }
    public string TimeZone { get; set; }
}