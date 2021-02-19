using DakarRally.Application.Interfaces;
using DakarRally.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DakarRally.Application.Services
{
    public class ExceptionLogger : IExceptionLogger
    {
        private readonly IDbContext _dbContext;

        public ExceptionLogger(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LoggException(Exception exception)
        {
            while (exception != null)
            {
                _dbContext.Insert(GetExceptionData(exception));

                exception = exception.InnerException;
            }

            await _dbContext.SaveChangesAsync();
        }

        private ErrorLog GetExceptionData(Exception exception)
        {
            return new ErrorLog()
            {
                Type = exception.GetType().ToString(),
                StackTrace = exception.StackTrace,
                Source = exception.Source,
                Message = exception.Message,
                InnerException = exception.InnerException == null ? string.Empty : exception.InnerException.GetType().ToString(),
                HResult = exception.HResult
            };
        }
    }
}
