using System;

namespace Pomelo.Kernel.Common
{
    public abstract class DeleteCommand : ICommand
    {
        protected DeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}