using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using EnsureThat;
using Microsoft.EntityFrameworkCore;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EntityFramework
{
    public abstract class BaseEntityFrameworkReadRepository<T> : IReadRepository<T>
        where T : AggregateRoot
    {
        private readonly IQueryable<T> entitiesQuery;

        protected BaseEntityFrameworkReadRepository(DbContext context)
        {
            EnsureArg.IsNotNull(context);
            this.entitiesQuery = this.DecorateEntities(context.Set<T>().AsQueryable());
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.entitiesQuery.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await this.entitiesQuery.Where(predicate).ToListAsync();
        }

        public async Task<Maybe<T>> FindOne(Expression<Func<T, bool>> predicate)
        {
            return await this.entitiesQuery.FirstOrDefaultAsync();
        }

        public async Task<Maybe<T>> GetById(Guid id)
        {
            return await this.entitiesQuery.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual IQueryable<T> DecorateEntities(IQueryable<T> entities) => entities;
    }
}