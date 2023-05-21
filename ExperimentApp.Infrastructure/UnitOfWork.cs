using ExperimentApp.Infrastructure.Interfaces;
using ExperimentApp.Infrastructure.Interfaces.IRepositories;
using ExperimentApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExperimentApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IExperimentRepository _experimentRepository;
        private bool _disposed;
        private readonly IConfiguration _configuration;



        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IExperimentRepository ExperimentRepository
        {
            get { return _experimentRepository ??= new ExperimentRepository(_transaction); }
        }


        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        public void RollBack()
        {
            try
            {
                _transaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                _transaction.Dispose();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _experimentRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}