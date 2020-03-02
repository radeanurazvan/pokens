using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Pokens.Battles.Domain;
using Pokens.Battles.Resources;
using Pomelo.Kernel.Domain;

namespace Pokens.Battles.Business
{
    internal sealed class ToggleAutoModeCommandHandler : IRequestHandler<ToggleAutoModeCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public ToggleAutoModeCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(ToggleAutoModeCommand request, CancellationToken cancellationToken)
        {
            return await mediator.ReadById<Trainer>(request.TrainerId).ToResult(Messages.TrainerNotFound)
                .Tap(t => t.ToggleAutoMode())
                .Tap(() => mediator.Write<Trainer>().Save());
        }
    }
}