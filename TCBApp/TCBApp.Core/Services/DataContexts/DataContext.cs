using Microsoft.EntityFrameworkCore;
using TCBApp.Models;

namespace TCBApp.Services.DataContexts;

public class DataContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatModel> Chats { get; set; }
    public DbSet<BoardModel> Boards { get; set; }

    public DataContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=tgbot; username=postgres; password=159357Dax");
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder
            .HasDefaultSchema("tgbot");
        
        modelBuilder
            .Entity<Message>()
            .Property(x => x.Content)
            .HasColumnType("jsonb");
        
        modelBuilder
            .Entity<Models.User>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();

        modelBuilder
            .Entity<Message>()
            .HasOne(x => x.Client)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.FromId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Message>()
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId);
        
        modelBuilder
            .Entity<Message>()
            .HasOne(x => x.Board)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.BoardId);

        modelBuilder
            .Entity<Client>()
            .HasOne(x => x.User)
            .WithOne(x => x.Client)
            .HasForeignKey<Client>(x => x.UserId);

        modelBuilder
            .Entity<Client>()
            .HasMany(x => x.Boards)
            .WithOne(x => x.Owner)
            .HasForeignKey(x => x.OwnerId);

        modelBuilder
            .Entity<Client>()
            .HasMany(x => x.FromConversations)
            .WithOne(x => x.From)
            .HasForeignKey(x => x.FromId);
        
        modelBuilder
            .Entity<Client>()
            .HasMany(x => x.ToConversations)
            .WithOne(x => x.To)
            .HasForeignKey(x => x.ToId);
    }
   
}
    
