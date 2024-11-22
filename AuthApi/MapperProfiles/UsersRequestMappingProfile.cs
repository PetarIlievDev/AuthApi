namespace AuthApi.MapperProfiles
{
    using AuthApi.Services.Models.Register;
    using AutoMapper;
    using RollingDiceApi.Models.LogIn;
    using RollingDiceApi.Models.UsersRegister;

    public class UsersRequestMappingProfile : Profile
    {
        public UsersRequestMappingProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUserServiceRequest>();
            CreateMap<LogInRequest, LogInServiceRequest>();
        }
    }
}
