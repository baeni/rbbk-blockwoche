using Microsoft.EntityFrameworkCore;
using Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.Contexts;

public class RelationalContext : DbContext
{
    private const string CONNECTION_STRING = "Host=localhost;Port=54321;Database=postgres-buechereien-rel;Username=postgres;Password=password1234";
    
    public DbSet<BuechereiRel> Buechereien => Set<BuechereiRel>();

    public DbSet<Autor> Autoren => Set<Autor>();

    public DbSet<Buch> Buecher => Set<Buch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuechereiRel>()
            .HasKey(b => b.BuechereiId);

        modelBuilder.Entity<Autor>()
            .HasKey(b => b.AutorId);

        modelBuilder.Entity<Buch>()
            .HasKey(b => b.BuchId);

        modelBuilder.Entity<BuechereiRel>()
            .ToTable("buechereien")
            .HasMany(b => b.Autoren)
            .WithMany(a => a.Buechereien);

        modelBuilder.Entity<Autor>()
            .ToTable("autoren")
            .HasMany(autor => autor.Buecher)
            .WithOne(buch => buch.Autor)
            .HasForeignKey(buch => buch.AutorId);

        modelBuilder.Entity<Buch>()
            .ToTable("buecher");


        modelBuilder.Entity<AutorBuecherei>()
            .HasKey(ab => new { ab.AutorId, ab.BuechereiId });


        modelBuilder.Entity<BuechereiRel>()
            .HasMany(e => e.Autoren)
            .WithMany(e => e.Buechereien)
            .UsingEntity<AutorBuecherei>();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql(CONNECTION_STRING);
    }
}
