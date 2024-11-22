namespace AuthApi.Services.MapperProfiles
{
    using AuthApi.DataAccess.Models;
    using AuthApi.Services.Models.Register;
    using AutoMapper;

    public class UserServiceMappingProfiles : Profile
    {
        public UserServiceMappingProfiles()
        {
            CreateMap<RegisterUserServiceRequest, RegisterUser>();
            CreateMap<LogInServiceRequest, LogInUser>();
        }
    }
}
