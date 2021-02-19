using System;
using System.Collections.Generic;

namespace DakarRally.Contracts
{
    /// <summary>
    /// Operation result, contains operation status error information.
    /// </summary>
    public class Result<TValue>
    {
        private readonly TValue _value;


        /// <summary>
        /// Initializes a new instance of the <see cref="Result"/> class with the specified parameters.
        /// </summary>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{Value}"/> class with the specified parameters.
        /// </summary>
        /// <param name="value">The result value.</param>
        /// <param name="isSuccess">The flag indicating if the result is successful.</param>
        /// <param name="errorList">The list of erros.</param>
        public Result(TValue value, bool isSuccess, List<string> errorList)
        {
            if (isSuccess && errorList != null)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && errorList == null)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            ErrorsList = errorList;
            _value = value;
        }
        /// <summary>
        /// Gets a value indicating whether the result is a success result.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the result is a failure result.
        /// </summary>
        public bool IsFailure { get { return !IsSuccess; } }

        /// <summary>
        /// List of errors.
        /// </summary>
        public List<string> ErrorsList { get; }

        /// <summary>
        /// Gets the result value if the result is successful, otherwise throws an exception.
        /// </summary>
        /// <returns>The result value if the result is successful.</returns>
        /// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is true.</exception>
        public TValue Value { get { return IsSuccess ? _value : throw new InvalidOperationException("The value of a failure result can not be accessed."); } }


        /// <summary>
        /// Returns a failure <see cref="Result{TValue}"/> with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error and failure flag set.</returns>
        public static Result<TValue> Failure(List<string> errorsList)
        {
            return new Result<TValue>(default, false, errorsList);
        }

        /// <summary>
        /// Returns a failure <see cref="Result{TValue}"/> with the specified error.
        /// </summary>
        /// <typeparam name="TValue">The result type.</typeparam>
        /// <param name="error">The error.</param>
        /// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error and failure flag set.</returns>
        public static Result<TValue> Success(TValue value)
        {
            return new Result<TValue>(value, false, null);
        }


    }
}
