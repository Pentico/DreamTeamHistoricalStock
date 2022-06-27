using System.Collections.Generic;
using System.Threading.Tasks;
using DreamTeam.Historical.Stock.Price.Dashboard.Application.Models;
using Newtonsoft.Json.Linq;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.Interfaces;

public interface ISignalService
{
    Task DailyPriceSymbolMessage(List<string> entries);
}