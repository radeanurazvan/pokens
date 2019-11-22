using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Infrastructure
{
    internal sealed class IdentityCredentialsService : ICredentialsService
    {
        private readonly UserManager<Credentials> manager;

        public IdentityCredentialsService(UserManager<Credentials> manager)
        {
            this.manager = manager;
        }

        public async Task<Result> Create(Guid trainerId, string email, string password)
        {
            var credentials = new Credentials(email, trainerId);
            var identityResult = await manager.CreateAsync(credentials, password);
            var error = identityResult.Errors.FirstOrDefault()?.Description ?? "Cannot create";

            return Result.Ok(identityResult)
                .Ensure(r => !r.Errors.Any(), error);
        }

        public Task<Result<Credentials>> GetByTuple(string email, string password)
        {
            var invalidCredentials = "Invalid credentials";

            return Result.Try(() => manager.FindByEmailAsync(email))
                .Ensure(c => c != null, invalidCredentials)
                .Ensure(c => manager.CheckPasswordAsync(c, password), invalidCredentials);
        }
    }
}