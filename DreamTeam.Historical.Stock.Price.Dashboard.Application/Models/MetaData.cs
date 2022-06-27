using System;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;

public class MetaData
{
    public string Information { get; set; }
    public  string Symbol { get; set; }
    public  DateTime LastRefreshed { get; set; }
    public  string OutputSize { get; set; }
    public string TimeZone { get; set; }
    public  string TimeSeries { get; set; }
}