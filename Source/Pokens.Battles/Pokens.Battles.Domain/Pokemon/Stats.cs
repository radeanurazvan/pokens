using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Pokens.Battles.Domain
{
    public sealed class Stats : ValueObject
    {
        public Stats(DefensiveStats defensive, OffensiveStats offensive)
        {
            Defensive = defensive;
            Offensive = offensive;
        }

        public DefensiveStats Defensive { get; private set; }

        public OffensiveStats Offensive { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Defensive;
            yield return this.Offensive;
        }
    }
}