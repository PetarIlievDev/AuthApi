namespace AuthApi.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using AuthApi.DataAccess;
    using AuthApi.DataAccess.Models;
    using AuthApi.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {

        public async Task<bool> ValidateIfUserExistAsync(string email, CancellationToken ct)
        {
            var user = await context
                .RegisterUsers
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync(ct);

            return user != null;
        }

        public async Task<bool> ValidateEmailAndPasswordAsync(string email, string password, CancellationToken ct)
        {
            var user = await context
                .RegisterUsers
                .Where(x => x.Email == email && x.Password == password)
                .FirstOrDefaultAsync(ct);

            return user != null;
        }

        public async Task<RegisterUser> GetUserHashedPasswordAsync(string email, CancellationToken ct)
        {
            var user = await context
                .RegisterUsers
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync(ct);

            return user;
        }

        public Task<int> DeleteUsersExpiredTokensAsync(string email, CancellationToken ct)
        {
            var deletedLogIns = context
                .LogInUsers
                .Where(x => x.Email == email && x.ExpireAt < DateTime.UtcNow)
                .ExecuteDeleteAsync(ct);

            return deletedLogIns;
        }

        public async Task<bool> SaveRegisteredUserAsync(RegisterUser registerUser, CancellationToken ct)
        {
            await context.RegisterUsers.AddAsync(registerUser, ct);
            var created = await context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> SaveLogInUserAsync(LogInUser loggedInUser, CancellationToken ct)
        {
            await context.LogInUsers.AddAsync(loggedInUser, ct);
            var created = await context.SaveChangesAsync();

            return created > 0;
        }
    }
}
