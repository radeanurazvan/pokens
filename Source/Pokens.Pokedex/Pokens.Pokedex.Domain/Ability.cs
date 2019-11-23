namespace Pokens.Pokedex.Domain
{
    public sealed class Ability : PokedexEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Damage { get; set; }

        public int RequiredLevel { get; set; }

        public int Cooldown { get; set; }
    }
}