namespace AuthApi.Services.Interfaces
{
    using System.Threading.Tasks;
    using AuthApi.Services.Models.LogIn;
    using AuthApi.Services.Models.Register;

    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterUserServiceRequest registerUserServiceRequest, CancellationToken ct);

        Task<LogInUserServiceResponse> LogInUser(LogInServiceRequest logInServiceRequest, CancellationToken ct);
    }
}
