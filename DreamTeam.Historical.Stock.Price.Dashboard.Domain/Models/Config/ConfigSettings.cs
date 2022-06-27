namespace DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.Config
{
    public class ConfigSettings
    {
        public string ConnectionStringDatabase { get; set; }
        public Guid SourceId { get; set; }
        public  string GetDailyPricesSchedule { get; set; }
        public string RapidApiKey { get; set; }
        public string RapidApiHost { get; set; }
        public string RapidClientUrl { get; set; }
    }
}