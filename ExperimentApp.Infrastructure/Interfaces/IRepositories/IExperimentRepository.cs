using ExperimentApp.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApp.Infrastructure.Interfaces.IRepositories
{
    public interface IExperimentRepository
    {
        Task<bool> AddExperimentAsync(Experiment entity);
        Task<IEnumerable<Experiment>> GetAllExperimentAsync();
        Task<Experiment?> GetByDeviceTokenAsync(string deviceToken, string key);
        Task<bool> RemoveExperimentByIdAsync(int id);
        Task<IEnumerable<ExperimentStatistics>> GetExperimentStatisticAsync();
    }
}
