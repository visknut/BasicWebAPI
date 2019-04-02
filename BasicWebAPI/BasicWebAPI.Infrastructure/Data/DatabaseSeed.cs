using BasicWebAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BasicWebAPI.Infrastructure.Data
{
    public static class Seed
    {
        public static void InitializeHomeInventoryDb(BasicWebAPIDbContext context, UserManager<User> userManager)
        {
            if (context.IsEmpty())
            {
                context.Examples.Add(new ExampleEntity());
                context.SaveChanges();
            }
        }
    }
}
