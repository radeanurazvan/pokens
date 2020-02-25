using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pokens.Battles.Domain
{
    public interface IBattlesService
    {
        Task<Result> TakeTurn(Guid battleId, Guid trainerId, Guid abilityId);
    }
}