using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;
using Pomelo.Kernel.Events.Abstractions;

namespace Pokens.Battles.Business
{
    internal sealed class BattleEndedEventHandler : IDomainEventHandler<BattleEndedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public BattleEndedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(BattleEndedEvent @event)
        {
            await mediator.ReadById<Trainer>(@event.Winner).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.AcknowledgeWonBattle(@event.BattleId, BattleExperience.ForWinner()));
            await mediator.ReadById<Trainer>(@event.Loser).ToResult(Messages.TrainerNotFound)
                .Bind(t => t.AcknowledgeLostBattle(@event.BattleId, BattleExperience.ForLoser()));
            await mediator.Write<Trainer>().Save();
        }

        private static class BattleExperience
        {
            private static readonly Random Random;

            static BattleExperience()
            {
                Random = new Random(DateTime.Now.Millisecond);
            }

            private const int MinBaseExperience = 50;
            private const int MaxBaseExperience = 100;

            private const int WinnerExperienceJitter = 50;
            private const double MinWinnerExperienceMultiplier = 1.5;
            private const double MaxWinnerExperienceMultiplier = 2.5;

            public static int ForLoser()
            {
                return Random.Next(MinBaseExperience, MaxBaseExperience);
            }

            public static int ForWinner()
            {
                var baseExperience = ForLoser() + WinnerExperienceJitter;
                var multiplier = Random.NextDouble() * (MaxWinnerExperienceMultiplier - MinWinnerExperienceMultiplier) + MinWinnerExperienceMultiplier;

                return (int)Math.Round(baseExperience * multiplier);
            }
        }
    }
}