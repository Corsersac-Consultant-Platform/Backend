using Microsoft.EntityFrameworkCore;
using Support.Models;

namespace Infrastructure.Persistence.EFC;

public class AppDbContext(DbContextOptions<AppDbContext>options)  : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<User>().Property(x => x.Username).IsRequired();
        modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
        
        modelBuilder.Entity<Role>().ToTable("roles");
        modelBuilder.Entity<Role>().HasKey(x => x.Id);
        modelBuilder.Entity<Role>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Role>().Property(x => x.Type).IsRequired();
        
        
        modelBuilder.Entity<User>().HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId);
    }
}