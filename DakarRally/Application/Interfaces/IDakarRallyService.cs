using DakarRally.Contracts;
using System;

namespace DakarRally.Application.Interfaces
{
    /// <summary>
    /// Dakar Rally aplication service abstract class 
    /// </summary>
    public abstract class DakarRallyService
    {
        /// <summary>
        /// Handles the ocurred exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Failure result</returns>
        protected abstract Result HandleException(Exception exception);

        /// <summary>
        /// Handles the ocurred exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>Failure result</returns>
        protected abstract Result<Value> HandleException<Value>(Exception exception);
    }
}
