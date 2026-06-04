using Data.Entites.Configuration;
using Data.Entites.EventSaver;
using Data.Entites.Languages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Shared.Models;

namespace Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole,string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>,IdentityUserToken<string>>(options)
{
    public DbSet<SupportedLanguage> SupportedLanguages { get; set; }
    public DbSet<ApplicationParameter> ApplicationParameters { get; set; }
    public DbSet<DictParameterType> DictParameterTypes { get; set; }
    public DbSet<SeasonDb> Seasons { get; set; }
    public DbSet<LineDb> Lines { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>(u =>
        {
            u.HasMany(r => r.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });
        builder.Entity<ApplicationRole>(u =>
        {
            u.HasMany(r => r.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });
    }
}