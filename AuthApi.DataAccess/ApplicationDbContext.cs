namespace AuthApi.DataAccess
{
    using AuthApi.DataAccess.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RegisterUser>()
                .HasIndex(user => user.UserId)
                .IsUnique();

            builder.Entity<LogInUser>()
                .HasOne(user => user.RegisterUser)
                .WithMany()
                .HasForeignKey(user => user.UserId);
        }

        public DbSet<LogInUser> LogInUsers { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
    }
}
