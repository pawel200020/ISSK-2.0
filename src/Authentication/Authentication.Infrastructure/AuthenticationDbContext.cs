using Authentication.Infrastructure.Entities.ExternalLoginProviders;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure;

internal class AuthenticationDbContext : DbContext
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }
    
    public DbSet<ExternalLoginProviderDbModel> ExternalLoginProviders { get; set; }

}