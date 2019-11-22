using CSharpFunctionalExtensions;

namespace Pokens.Trainers.Domain
{
    public interface ITokenService
    {
        Result<AuthenticationToken> Exchange(Credentials credentials);
    }
}