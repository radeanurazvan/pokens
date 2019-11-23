using System;
using CSharpFunctionalExtensions;
using Pokens.Trainers.Common;
using Pomelo.Kernel.Common;
using Pomelo.Kernel.Domain;

namespace Pokens.Trainers.Domain
{
    public class Trainer : AggregateRoot
    {
        private Trainer()
        {
        }

        public static Result<Trainer> Create(string name, User user)
        {
            var nameResult = name.EnsureValidString(Messages.InvalidName);
            var userResult = user.EnsureExists(Messages.InvalidUser);
            return Result.FirstFailureOrSuccess(nameResult, userResult)
                .Map(() => new Trainer { Name = name, UserId = user.Id })
                .Tap(t => t.AddDomainEvent(new TrainerCreatedEvent(t)));
        }

        public string Name { get; private set; }

        public User User { get; private set; }

        public Guid UserId { get; private set; }
    }
}