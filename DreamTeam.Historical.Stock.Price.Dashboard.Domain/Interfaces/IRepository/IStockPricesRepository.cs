using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Models.FilterCriterias;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IRepository;

public interface IStockPricesRepository
{
    IList<StockPriceModel> GetAllStockPricesList(StockPricesFilterCriteria filterCriteria);

    bool InsertStockPrice(StockPriceModel entry);
    
}