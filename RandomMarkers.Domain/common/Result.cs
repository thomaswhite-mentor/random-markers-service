using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomMarkers.Domain.common
{
    public class Result
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public bool Failure
        {
            get { return !Success; }
        }
        protected Result(bool success, string error)
        {
            Guard.Require(success || !string.IsNullOrEmpty(error), "Error message is not required because the result is success");
            Guard.Require(!success || string.IsNullOrEmpty(error), "Error message is required if the success is false");
            Success = success;
            Error = error;
        }
        public static Result Fail(string message)
        {
            return new Result(false, message);
        }
        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }
        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(false, message);
        }
    }
    public class Result<T> : Result
    {
        public T? Value { get; }

        public Result(bool success, string errorMessage, T value) : base(success, errorMessage)
        {
            Value = value;
        }
        public Result(bool success, string errorMessage) : base(success, errorMessage)
        {
            Value = default;
        }
    }
}
