using System;
using CSharpFunctionalExtensions;

namespace Pomelo.Kernel.Domain
{
    public interface IIdentifiedUser
    {
        Maybe<Guid> Id { get; }
    }
}