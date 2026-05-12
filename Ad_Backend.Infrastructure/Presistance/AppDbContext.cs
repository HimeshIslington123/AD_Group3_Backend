using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Presistance;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

  
    public DbSet<Staff> Staffs { get; set; }


}