using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Pokens.Trainers.Domain
{
    public interface IUsersService
    {
        Task<Result> Create(User user, string password);

        Task<Result<User>> GetByCredentials(string email, string password);
    }
}