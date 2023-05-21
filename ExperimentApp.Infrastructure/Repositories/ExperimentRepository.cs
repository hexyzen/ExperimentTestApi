using ExperimentApp.Common.Models;
using ExperimentApp.Infrastructure.Interfaces.IRepositories;
using ExperimentApp.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;
using Dapper;
using System.Threading.Tasks;
using System.Transactions;

namespace ExperimentApp.Infrastructure.Repositories
{
    public class ExperimentRepository : BaseRepository, IExperimentRepository
    {
        public ExperimentRepository(IDbTransaction transaction)
         : base(transaction)
        {
        }

        public async Task<bool> AddExperimentAsync(Experiment entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await Connection.ExecuteAsync(
            "INSERT INTO [Experiment](DeviceToken, [Key], Value) VALUES(@DeviceToken, @Key, @Value);  SELECT SCOPE_IDENTITY()",
            param: new { DeviceToken = entity.DeviceToken, Key = entity.Key, Value = entity.Value },
            transaction: Transaction
            );
            return true;
        }

        public async Task<IEnumerable<Experiment>> GetAllExperimentAsync()
        {
            return await Connection.QueryAsync<Experiment>(
                  "SELECT * FROM [Experiment]",
                   transaction: Transaction
              );
        }

        public async Task<bool> RemoveExperimentByIdAsync(int id)
        {
            await Connection.ExecuteAsync(
            "DELETE FROM Experiment WHERE Id = @Id",
            param: new { Id = id },
            transaction: Transaction
            );
            return true;
        }


        public async Task<Experiment> GetByDeviceTokenAsync(string deviceToken, string key)
        {
            return await Connection.QueryFirstOrDefaultAsync<Experiment>(
                "SELECT * FROM [Experiment] WHERE DeviceToken = @DeviceToken AND [Key] = @Key",
            param: new { DeviceToken = deviceToken, Key = key },
            transaction: Transaction
            );
        }

        public async Task<IEnumerable<ExperimentStatistics>> GetExperimentStatisticAsync()
        {
            return await Connection.QueryAsync<ExperimentStatistics>(
                @"SELECT [Key], COUNT(DISTINCT [DeviceToken]) AS DeviceCount, [Value], COUNT(*) AS OptionCount
                    FROM [Experiment]
                    GROUP BY [Key], [Value]",
                transaction: Transaction
              );
        }

    }
}
