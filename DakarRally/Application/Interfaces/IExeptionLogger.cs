using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DakarRally.Application.Interfaces
{
    public interface IExceptionLogger
    {
        Task LoggException(Exception exception);
    }
}
