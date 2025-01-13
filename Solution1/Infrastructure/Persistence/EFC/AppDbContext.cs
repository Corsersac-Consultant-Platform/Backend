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
      modelBuilder.Entity<User>().Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
      modelBuilder.Entity<User>().Property(x => x.Username).IsRequired().HasColumnName("username");
      modelBuilder.Entity<User>().Property(x => x.Password).IsRequired().HasColumnName("password");
        modelBuilder.Entity<User>().Property(x => x.RoleId).IsRequired().HasColumnName("role_id");
      modelBuilder.Entity<User>().HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId);
      
      modelBuilder.Entity<Role>().ToTable("roles");
      modelBuilder.Entity<Role>().HasKey(x => x.Id);
      modelBuilder.Entity<Role>().Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
      modelBuilder.Entity<Role>().Property(x => x.Type).IsRequired().HasColumnName("type");

        
      modelBuilder.Entity<Status>().ToTable("statuses");
      modelBuilder.Entity<Status>().HasKey(x => x.Id);
      modelBuilder.Entity<Status>().Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
      modelBuilder.Entity<Status>().Property(x => x.Type).IsRequired().HasColumnName("type");

    
      modelBuilder.Entity<Invoice>().ToTable("invoices");
      modelBuilder.Entity<Invoice>().HasKey(x => x.Id);
      modelBuilder.Entity<Invoice>().Property(x => x.Id).ValueGeneratedOnAdd().HasColumnName("id");
      modelBuilder.Entity<Invoice>().Property(x => x.EmitDate).IsRequired().HasColumnName("emit_date");
      modelBuilder.Entity<Invoice>().Property(x => x.Amount).IsRequired().HasColumnName("amount");
      modelBuilder.Entity<Invoice>().Property(x => x.Name).IsRequired().HasColumnName("name");
      modelBuilder.Entity<Invoice>().Property(x => x.Number).IsRequired().HasColumnName("number");
      modelBuilder.Entity<Invoice>().Property(x => x.Register).IsRequired().HasColumnName("register");
      modelBuilder.Entity<Invoice>().Property(x => x.Serie).IsRequired().HasColumnName("serie");
        modelBuilder.Entity<Invoice>().Property(x => x.StatusId).IsRequired().HasColumnName("status_id");
      modelBuilder.Entity<Invoice>().HasOne<Status>().WithMany().HasForeignKey(x => x.StatusId);

    }
}