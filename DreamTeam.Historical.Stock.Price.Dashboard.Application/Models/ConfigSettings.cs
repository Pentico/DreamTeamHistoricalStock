namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;

public class ConfigSettings
{
    public string GetDailyPricesSchedule { get; set; }
    public string RapidApiKey { get; set; }
    public  string RapidApiHost { get; set; }
    public  string RapidClientUrl { get; set; }
    public  string ConnectionString { get; set; }
    
    public  string[] CompanySymbolList { get; set; }
}