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

        public static Result<Trainer> Create(string name)
        {
            return name.EnsureValidString(Messages.InvalidName)
                .Map(n => new Trainer { Name = name })
                .Tap(t => t.AddDomainEvent(new TrainerCreatedEvent(t)));
        }

        public string Name { get; private set; }
    }
}