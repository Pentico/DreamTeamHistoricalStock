using Dapper;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IRepository
{
    public interface IRepositoryBase<TFilterCriteria>
    {
        IDatabaseConnectionFactory ConnectionFactory { get; }

        void ApplyFilter(SqlBuilder builder, TFilterCriteria filterCriteria);
        
    }
}