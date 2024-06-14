using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Items> Items { get; set; }
    public DbSet<Backpacks> Backpacks { get; set; }
    public DbSet<Characters> Characters { get; set; }
    public DbSet<CharactersTitles> CharactersTitles { get; set; }
    public DbSet<Titles> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Characters>().HasData(
            new Characters { Id = 1, FirstName = "FirstName1", LastName = "LastName1", CurrentWeight = 30, MaxWeight = 170 },
            new Characters { Id = 2, FirstName = "FirstName2", LastName = "LastName2", CurrentWeight = 40, MaxWeight = 180 }

        );

        modelBuilder.Entity<Items>().HasData(
            new Items { Id = 1, Name = "Item1", Weight = 9 },
            new Items { Id = 2, Name = "Item2", Weight = 10 },
            new Items { Id = 3, Name = "Item3", Weight = 11 }
        );

        modelBuilder.Entity<Backpacks>().HasData(
            new Backpacks { CharacterId = 1, ItemId = 1, Amount = 2 },
            new Backpacks { CharacterId = 2, ItemId = 2, Amount = 1 },
            new Backpacks { CharacterId = 1, ItemId = 3, Amount = 4 }
        );

        modelBuilder.Entity<Titles>().HasData(
            new Titles { Id = 1, Name = "Title1" },
            new Titles { Id = 2, Name = "Title2" },
            new Titles { Id = 3, Name = "Title3" }
        );

        modelBuilder.Entity<CharactersTitles>().HasData(
            new CharactersTitles { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2000, 8, 11) },
            new CharactersTitles { CharacterId = 1, TitleId = 2, AcquiredAt = new DateTime(2055, 3, 1) },
            new CharactersTitles { CharacterId = 2, TitleId = 3, AcquiredAt = new DateTime(1990, 9, 24) }
        );
    }
}