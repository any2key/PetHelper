using Microsoft.EntityFrameworkCore;
using PetHelper.Models.Buisness;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;

namespace WebKeyGenerator.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Specialty> Specialties { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Login = "admin",
                        Active = true,
                        Role = "admin",
                        Email = "admin@admin.admin",
                        Password = "38ef/vWYE15AgcPSeUkYA3rXsVgnnGEtj0xWxq846bI=",
                        PasswordSalt = "7KmMVPksY91X8kfcxsceURd71psvDgJ24vsRw5aKrVA="
                    }

            );
        }
    }
}
