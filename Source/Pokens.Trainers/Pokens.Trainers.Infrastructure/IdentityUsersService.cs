using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Pokens.Trainers.Domain;

namespace Pokens.Trainers.Infrastructure
{
    internal sealed class IdentityUsersService : IUsersService
    {
        private readonly UserManager<User> manager;

        public IdentityUsersService(UserManager<User> manager)
        {
            this.manager = manager;
        }

        public async Task<Result> Create(User user, string password)
        {
            var identityResult = await manager.CreateAsync(user, password);
            var error = identityResult.Errors.FirstOrDefault()?.Description ?? "Cannot create";

            return Result.Ok(identityResult)
                .Ensure(r => !r.Errors.Any(), error);
        }

        public Task<Result<User>> GetByCredentials(string email, string password)
        {
            var invalidCredentials = "Invalid credentials";

            return Result.Try(() => manager.FindByEmailAsync(email))
                .Ensure(c => c != null, invalidCredentials)
                .Ensure(c => manager.CheckPasswordAsync(c, password), invalidCredentials);
        }
    }
}