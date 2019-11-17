using Microsoft.EntityFrameworkCore;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EntityFramework
{
    internal sealed class EntityFrameworkReadRepository<T> : BaseEntityFrameworkReadRepository<T>
        where T : AggregateRoot
    {
        public EntityFrameworkReadRepository(DbContext context) 
            : base(context)
        {
        }
    }
}