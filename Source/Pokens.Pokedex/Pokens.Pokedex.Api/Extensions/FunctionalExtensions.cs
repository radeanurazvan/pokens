using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Pokens.Pokedex.Api.Extensions
{
    public static class FunctionalExtensions
    {
        public static IActionResult ToActionResult(this Result result, Func<IActionResult> onOk)
        {
            if (result.IsSuccess)
            {
                return onOk();
            }

            return new BadRequestObjectResult(new ApiResult(result));
        }

        public static IActionResult ToActionResult<T>(this Result<T> result, Func<IActionResult> onOk)
            where T : class
        {
            if (result.IsSuccess)
            {
                return onOk();
            }

            return new BadRequestObjectResult(new ApiResult(result));
        }
    }
}
