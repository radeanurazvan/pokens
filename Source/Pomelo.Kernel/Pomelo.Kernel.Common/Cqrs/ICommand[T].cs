using CSharpFunctionalExtensions;
using MediatR;

namespace Pomelo.Kernel.Common
{
    public interface ICommand<T> : IRequest<Result<T>>
    {
        
    }
}