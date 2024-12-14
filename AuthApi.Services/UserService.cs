namespace AuthApi.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Web.Helpers;
    using AuthApi.DataAccess.Models;
    using AuthApi.Repositories.Interfaces;
    using AuthApi.Services.Interfaces;
    using AuthApi.Services.Models.LogIn;
    using AuthApi.Services.Models.Register;
    using AutoMapper;
    using Microsoft.IdentityModel.Tokens;

    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        public async Task<bool> RegisterUser(RegisterUserServiceRequest registerUserServiceRequest, CancellationToken ct)
        {
            var employeeDataToSave = mapper.Map<RegisterUser>(registerUserServiceRequest);
            var userExists = await userRepository.ValidateIfUserExistAsync(employeeDataToSave.Email, ct);

            if (userExists)
            {
                throw new Exception("User already exists");
            }

            employeeDataToSave.Password = Crypto.HashPassword(registerUserServiceRequest.Password);

            var result = await userRepository.SaveRegisteredUserAsync(employeeDataToSave, ct);

            if (!result)
            {
                throw new Exception("Failed to save user");
            }

            return true;
        }

        public async Task<LogInUserServiceResponse> LogInUser(LogInServiceRequest logInServiceRequest, CancellationToken ct)
        {
            var userExists = await userRepository.ValidateIfUserExistAsync(logInServiceRequest.Email, ct);

            if (!userExists)
            {
                throw new Exception("UserName or Password is wrong");
            }

            var user = await userRepository.GetUserHashedPasswordAsync(logInServiceRequest.Email, ct) ?? throw new UnauthorizedAccessException("UserName or Password is wrong");

            if(!Crypto.VerifyHashedPassword(user.Password, logInServiceRequest.Password))
            {
                throw new UnauthorizedAccessException("UserName or Password is wrong");
            }

            await userRepository.DeleteUsersExpiredTokensAsync(logInServiceRequest.Email, ct);
            var logInRequest = mapper.Map<LogInUser>(logInServiceRequest);
            logInRequest.Token = GenerateJwtToken(logInServiceRequest.Email);
            logInRequest.UserId = user.UserId;
            logInRequest.ExpireAt = DateTime.UtcNow.AddMinutes(60);

            var success = await userRepository.SaveLogInUserAsync(logInRequest, ct);

            if (success) 
            { 
                return new LogInUserServiceResponse { Token = logInRequest.Token };
            }

            return null;
        }

        private static string GenerateJwtToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the_key_here_is_not_secured_enought_:)"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "i_am_the_issuer",
                audience: "and_i_am_the_audience",
                claims: [ new Claim(ClaimTypes.Email, email) ],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
