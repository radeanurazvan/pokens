using System;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Infrastructure
{
    internal sealed class ClaimsIdentifiedUser : IIdentifiedUser
    {
        private readonly HttpContext context;

        public ClaimsIdentifiedUser(IHttpContextAccessor accessor)
        {
            this.context = accessor.HttpContext;
        }

        public Maybe<Guid> Id => context.User.Claims.FirstOrNothing(c => c.Type == "TrainerId")
            .Select(c => new Guid(c.Value));
    }
}