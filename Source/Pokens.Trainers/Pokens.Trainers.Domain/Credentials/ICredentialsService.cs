using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pokens.Trainers.Domain
{
    public interface ICredentialsService
    {
        Task<Result> Create(Guid trainerId, string email, string password);

        Task<Result<Credentials>> GetByTuple(string email, string password);
    }
}