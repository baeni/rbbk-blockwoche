using Microsoft.EntityFrameworkCore;
using Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.Contexts;

public class RelationalContext : DbContext
{
    public DbSet<EntitiesRelational.Bücherei> Büchereien => Set<EntitiesRelational.Bücherei>();

    public DbSet<Autor> Autoren => Set<Autor>();

    public DbSet<Buch> Bücher => Set<Buch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntitiesRelational.Bücherei>()
            .HasMany(b => b.Autoren)
            .WithMany(a => a.Büchereien);

        modelBuilder.Entity<Autor>()
            .HasMany(autor => autor.Bücher)
            .WithOne(buch => buch.Autor)
            .HasForeignKey(buch => buch.AutorId);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=54321;Database=postgres-buechereien;Username=postgres;Password=password1234");
    }
}
