﻿using CSharpFunctionalExtensions;

namespace Pokens.Pokedex.Api.Extensions
{
    public class ApiResult
    {
        public ApiResult(Result result)
        {
            IsSuccess = result.IsSuccess;
            IsFailure = result.IsFailure;
            Error = result.IsFailure ? result.Error : "";
        }

        public bool IsSuccess { get; private set; }

        public bool IsFailure { get; private set; }

        public string Error { get; set; }
    }

    public class GenericApiResult<T>
        where T : class
    {
        public GenericApiResult(Result<T> result)
        {
            IsSuccess = result.IsSuccess;
            IsFailure = result.IsFailure;
            Error = result.IsFailure ? result.Error : "";
            Value = result.IsSuccess ? result.Value : null;
        }

        public bool IsSuccess { get; private set; }

        public bool IsFailure { get; private set; }

        public string Error { get; set; }

        public T Value { get; set; }
    }
}
