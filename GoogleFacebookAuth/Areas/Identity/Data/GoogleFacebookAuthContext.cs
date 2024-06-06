using GoogleFacebookAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoogleFacebookAuth.Data;

public class GoogleFacebookAuthContext : IdentityDbContext<IdentityUser>
{
    public GoogleFacebookAuthContext(DbContextOptions<GoogleFacebookAuthContext> options)
        : base(options)
    {
    }


    //Adding Domain Classes as DbSet Properties
	public DbSet<Employee> Employees { get; set; }
	public DbSet<Customer> Customers { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
