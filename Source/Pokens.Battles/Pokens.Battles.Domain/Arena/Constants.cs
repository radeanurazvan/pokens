using System.Collections.Generic;

namespace Pokens.Battles.Domain
{
    public static class Constants
    {
        public static IEnumerable<Arena> DefaultArenas { get; } = new List<Arena>
        {
            Arena.Open("Jade Arena", 0).Value,
            Arena.Open("Crystal Arena", 5).Value,
            Arena.Open("Saphire Arena", 10).Value
        };
    }
}