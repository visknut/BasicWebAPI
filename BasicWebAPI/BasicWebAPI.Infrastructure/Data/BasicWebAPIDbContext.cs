using Microsoft.EntityFrameworkCore;
using BasicWebAPI.Core.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BasicWebAPI.Core.Shared;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BasicWebAPI.Infrastructure.Data
{
    public class BasicWebAPIDbContext : IdentityDbContext<User>
    {
        public DbSet<ExampleEntity> Examples { get; set; }

        public BasicWebAPIDbContext(DbContextOptions<BasicWebAPIDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExampleEntity>().Property(i => i.Status)
                .HasConversion(new EnumToStringConverter<Status>());
        }

        public bool IsEmpty()
        {
            return !Examples.Any();
        }
    }
}

