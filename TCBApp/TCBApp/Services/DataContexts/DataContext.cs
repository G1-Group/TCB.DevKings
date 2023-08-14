using Microsoft.EntityFrameworkCore;
using TCBApp.Models;
using User = Telegram.Bot.Types.User;

namespace TCBApp.Services.DataContexts;

public class DataContext:DbContext
{
    public DbSet<Models.User> users { get; set; }
    public DbSet<Models.Client> clients { get; set; }

    public DataContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=postgres; username=postgres; password=3214");
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        
        modelBuilder
            .Entity<Models.User>()
            .Property(x => x.Password)
            .IsRequired()
            .HasColumnName("password")
            .HasDefaultValue(null);
        
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        
        modelBuilder
            .Entity<Models.Client>()
            .Property(x => x.Nickname)
            .IsRequired()
            .HasColumnName("nickname")
            .HasDefaultValue(null);
        
        modelBuilder
            .Entity<Models.User>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();
        
        modelBuilder
            .Entity<Models.User>()
            .HasIndex(x => x.UserId)
            .IsUnique();
        
         modelBuilder
            .Entity<Models.User>()
            .Property(x => x.Password)
            .HasConversion(
                s => s.ToString(),
                s => s.ToString());
       
    }
   
}
    
