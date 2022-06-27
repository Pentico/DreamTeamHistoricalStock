using Dapper;
using DreamTeam.Historical.Stock.Price.Dashboard.Domain.Interfaces.IFactories;

namespace DreamTeam.Historical.Stock.Price.Dashboard.Data.Repositories
{
    
    public abstract class RepositoryBase<TEntity, TFilterCriteria>
    {
        protected IDatabaseConnectionFactory ConnectionFactory { get; }

        public abstract void ApplyFilter(SqlBuilder builder, TFilterCriteria filterCriteria);

        protected RepositoryBase(IDatabaseConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }
        
        protected bool ExistInDb(Guid id, string tableName)
        {
            using var connection = ConnectionFactory.GetConnection();
            var item = connection.QuerySingleOrDefault<dynamic>($"SELECT  (id) FROM {tableName} WHERE id=@id",
                new { id });
            return item != null;
        }
        
        protected int Insert(string sql, object @params)
        {
            using var connection = ConnectionFactory.GetConnection();
            return  connection.Execute(sql, @params);
        }
    }
}