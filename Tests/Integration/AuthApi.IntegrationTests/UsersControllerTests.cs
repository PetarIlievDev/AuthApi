namespace AuthApi.IntegrationTests
{
    using AuthApi.Controllers;
    using AuthApi.DataAccess;
    using AuthApi.Repositories.Interfaces;
    using AuthApi.Repositories;
    using AuthApi.Services.Interfaces;
    using AuthApi.Services;
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using AuthApi.Models;
    using RollingDiceApi.Models.UsersRegister;
    using AuthApi.MapperProfiles;
    using Microsoft.AspNetCore.Mvc;

    public class UsersControllerTests
    {
        private WebApplication app;
        private ServiceProvider serviceProvider;
        private IMapper mapper;
        private IUserService userService;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<UsersRequestMappingProfile>();
            });
            mapper = config.CreateMapper();
            userService = serviceProvider.GetRequiredService<IUserService>();
        }

        [Test]
        public async Task PostAsync_Register_Sucess_Result()
        {
            var controller = new UsersController(mapper, userService);
            var request = new RegisterUserRequest()
            {
                Email = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                Password = "password",
                ConfirmPassword = "password"
            };

            var response = await controller.PostAsync(request, CancellationToken.None);
            var contentResult = response as OkObjectResult;
            var result = contentResult?.Value as ResponseModel<string>;
            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(contentResult, Is.Not.Null);
                Assert.That(result, Is.Not.Null);
                Assert.That(result?.Status, Is.EqualTo(true));
                Assert.That(result?.Message, Is.EqualTo("success"));
                Assert.That(result?.Data, Is.EqualTo("User created successfully!"));
            });
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var builder = SetUpBuilder();
            app = builder.Build();
            serviceProvider = builder.Services.BuildServiceProvider();
            CreateDatabase(app);
        }

        [OneTimeTearDown]
        public void OneTimeTearDownAttribute()
        {
            serviceProvider?.Dispose();

            DeleteDatabaseData(app);

            app?.DisposeAsync();
        }


        private WebApplicationBuilder SetUpBuilder()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }

        private static void CreateDatabase(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();
        }

        private static void DeleteDatabaseData(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureDeleted();
        }
    }
}