using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pomelo.Kernel.Common
{
    public static class Extensions
    {
        public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            try
            {
                return collection.First(predicate);
            }
            catch
            {
                return Maybe<T>.None;
            }
        }

        public static Result<string> EnsureValidString(this string subject, string error)
        {
            return Result.FailureIf(string.IsNullOrEmpty(subject?.Trim()), subject, error);
        }

        public static Result<string> EnsureValidRegex(this string pattern, string error)
        {
            return pattern.EnsureValidString(error)
                .OnSuccessTry(p => Regex.Match("", p))
                .Map(_ => pattern);
        }

        public static Result<string> EnsureMatchesPattern(this Result<string> subject, string pattern, string error)
        {
            return subject.Ensure(v => Regex.IsMatch(v, pattern), error);
        }

        public static Result<string> EnsureMatchesPattern(this string subject, string pattern, string error)
        {
            return subject.EnsureValidString(error)
                .EnsureMatchesPattern(pattern, error);
        }

        public static Result<T> EnsureExists<T>(this T subject, string error)
        {
            return Result.Create(subject != null, subject, error);
        }

        public static Result<Guid> EnsureNotEmpty(this Guid subject, string error)
        {
            return Result.FailureIf(subject == Guid.Empty, subject, error);
        }

        public static Maybe<T> ToMaybe<T>(this T x)
            where T : class
        {
            return x;
        }

        public static Maybe<T> ToMaybe<T>(this Result<T> result)
        {
            if (result.IsFailure)
            {
                return Maybe<T>.None;
            }

            return result.Value;
        }

        public static async Task<Maybe<T>> ToMaybe<T>(this Task<Result<T>> resultTask)
        {
            return (await resultTask).ToMaybe();
        }

        public static async Task<Maybe<T>> ToMaybe<T>(this Task<T> task)
        {
            return await task;
        }

        public static async Task<K> Unwrap<T, K>(this Task<Maybe<T>> subjectTask, Func<T, K> selector, K defaultValue = default)
        {
            var maybe = await subjectTask;
            return maybe.Unwrap(selector, defaultValue);
        }

        public static IServiceCollection AddSingletonSettings<T>(this IServiceCollection services)
            where T : class
        {
            return services.AddSingleton(ctx =>
            {
                var configuration = ctx.GetService<IConfiguration>();
                var section = configuration.GetSection(typeof(T).Name);

                return section.Get<T>(o => o.BindNonPublicProperties = true);
            });
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                collection.Add(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> act)
        {
            foreach (var item in enumerable)
            {
                act(item);
            }
        }

        public static void Merge<T>(this ICollection<T> original, IEnumerable<T> changed, Action<T, T> conflictHandler)
            where T : class
        {
            var newEntries = changed.Except(original).ToList();
            newEntries.ForEach(original.Add);

            var updatedEntries = changed.Intersect(original).ToList();
            updatedEntries.ForEach(ue =>
            {
                var unchanged = original.First(x => x == ue);
                conflictHandler(unchanged, ue);
            });

            var deletedEntries = original.Except(changed).ToList();
            deletedEntries.ForEach(de => original.Remove(de));
        }

        public static Result FirstFailureOrSuccess(this IEnumerable<Result> results)
        {
            return Result.FirstFailureOrSuccess(results.ToArray());
        }

        public static Result<IEnumerable<T>> FirstFailureOrSuccess<T>(this IEnumerable<Result<T>> results)
        {
            var firstFailure = results.FirstOrNothing(r => r.IsFailure);
            return firstFailure.Unwrap(r => Result.Failure<IEnumerable<T>>(r.Error), Result.Success(results.Select(r => r.Value)));
        }

        public static IEnumerable<T> Collect<T>(this IEnumerable<Result<T>> results)
        {
            return results.Where(r => r.IsSuccess)
                .Select(r => r.Value);
        }

        public static string ToJson<T>(this T x)
        {
            return JsonSerializer.Serialize(x);
        }
    }
}