using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Entities.Models
{
    public class CustomIdentityDbContext:IdentityDbContext<CustomIdentityUser,CustomIdentityRole,string>
    {
         public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options) :base(options)
        {

        }
    }
}
