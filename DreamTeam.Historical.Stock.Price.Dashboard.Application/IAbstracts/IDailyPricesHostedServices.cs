using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Application.IAbstracts;

public abstract class IDailyPricesHostedServices: BackgroundService
{
    protected abstract Task<bool> VoidGetDailyPrices();
}