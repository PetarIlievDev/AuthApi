namespace AuthApi.Repositories.Interfaces
{
    using System.Threading.Tasks;
    using AuthApi.DataAccess.Models;

    public interface IUserRepository
    {
        Task<bool> ValidateIfUserExistAsync(string email, CancellationToken ct);
        Task<bool> ValidateEmailAndPasswordAsync(string email, string password, CancellationToken ct);
        Task<RegisterUser> GetUserHashedPasswordAsync(string email, CancellationToken ct);
        Task<int> DeleteUsersExpiredTokensAsync(string email, CancellationToken ct);
        Task<bool> SaveRegisteredUserAsync(RegisterUser registerUser, CancellationToken ct);
        Task<bool> SaveLogInUserAsync(LogInUser loggedInUser, CancellationToken ct);
    }
}
