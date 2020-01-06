using System.Reflection;

namespace Pokens.Battles.Domain
{
    public static class DomainAssembly
    {
        public static Assembly Value => typeof(DomainAssembly).Assembly;
    }
}