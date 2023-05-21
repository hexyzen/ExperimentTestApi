using ExperimentApp.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentApp.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IExperimentRepository ExperimentRepository { get; }
        void Commit();
        void RollBack();
    }

}