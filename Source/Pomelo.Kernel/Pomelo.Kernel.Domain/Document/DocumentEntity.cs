using System;

namespace Pomelo.Kernel.Domain
{
    public abstract class DocumentEntity
    {
        protected DocumentEntity()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; protected set; }
    }
}