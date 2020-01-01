using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.Infrastructure
{
    internal sealed class RepositoryMediator : IRepositoryMediator
    {
        private readonly IServiceProvider provider;

        public RepositoryMediator(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public IReadRepository<T> Read<T>() where T : AggregateRoot
        {
            return this.provider.GetService<IReadRepository<T>>();
        }

        public Task<Maybe<T>> ReadById<T>(Guid id) where T : AggregateRoot
        {
            return this.provider.GetService<IGetById<T>>().GetById(id);
        }

        public IWriteRepository<T> Write<T>() where T : AggregateRoot
        {
            return this.provider.GetService<IWriteRepository<T>>();
        }
    }
}