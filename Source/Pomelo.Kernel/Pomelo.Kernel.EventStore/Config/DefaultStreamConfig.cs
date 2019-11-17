using System;
using System.Text.RegularExpressions;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    internal sealed class DefaultStreamConfig<T> : IStreamConfig<T>
        where T : AggregateRoot
    {
        public string GetStreamFor(Guid aggregateId)
        {
            return $"{PascalCaseToDashCase(typeof(T).Name)}-{aggregateId}";
        }

        private static string PascalCaseToDashCase(string pascal)
        {
            return Regex.Replace(pascal, @"([a-z])([A-Z])", "$1-$2").ToLower();
        }
    }
}