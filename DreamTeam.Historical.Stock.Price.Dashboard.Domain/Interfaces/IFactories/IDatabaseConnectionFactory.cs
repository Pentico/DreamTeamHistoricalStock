using System.Data;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();
    }
}