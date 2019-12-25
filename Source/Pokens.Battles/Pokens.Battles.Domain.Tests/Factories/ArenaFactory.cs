namespace Pokens.Battles.Domain.Tests 
{
    public static class ArenaFactory
    {
        public static Arena WithoutRequirement() => Arena.Open("Test", 0).Value;

        public static Arena RequiringLevel(int level) => Arena.Open("Test", level).Value;
    }
}