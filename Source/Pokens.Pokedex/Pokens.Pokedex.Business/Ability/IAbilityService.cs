namespace Pokens.Pokedex.Business
{
    public interface IAbilityService
    {
        void Create(string name, string description, int damage, int requiredLevel, int cooldown);
    }
}