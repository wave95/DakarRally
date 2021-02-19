using System;
using System.Collections.Generic;

namespace DakarRally.Contracts
{
    /// <summary>
    /// Operation result, contains operation status error information.
    /// </summary>
    public class Result
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
        /// </summary>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="errorList">The list of erros.</param>
        public Result(bool isSuccess, List<string> errorsList)
        {
            if (isSuccess && errorsList != null && errorsList.Count > 0)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && (errorsList == null || errorsList.Count == 0))
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            ErrorsList = errorsList;

        }

        /// <summary>
        /// Flag indicating whether the result is a success result.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Flag indicating whether the result is a failure result.
        /// </summary>
        public bool IsFailure { get { return !IsSuccess; } }
            

        /// <summary>
        /// List of errors.
        /// </summary>
        public List<string> ErrorsList { get; }

        // <summary>
        /// Returns a success instance of <see cref="Result"/>.
        /// </summary>
        public static Result Success
        {
            get { return new Result(true, null); }
        }

        /// <summary>
        /// Returns a failure instance of <see cref="Result"/> with the specified error.
        /// </summary>
        /// <param name="errorsList">The error.</param>
        public static Result Failure(List<String> errorsList)
        {
            return new Result(false, errorsList);
        }

    }
}
