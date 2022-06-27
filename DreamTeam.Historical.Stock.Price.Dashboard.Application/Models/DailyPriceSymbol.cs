
using System.Collections.Generic;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;

public class DailyPriceSymbol
{
    public MetaData MetaData { get; set; }
    public Dictionary<string, dynamic> TimeSeriesDaily { get; set; }
}