using Microsoft.EntityFrameworkCore;
using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bücherei.Lib.Contexts;

public class RelationalContext : DbContext
{
    public DbSet<BuechereiRel> Buechereien => Set<BuechereiRel>();

    public DbSet<Autor> Autoren => Set<Autor>();

    public DbSet<Buch> Buecher => Set<Buch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        var data = SampleData.GetRel();


        //var author = new Rel.Autor() { Id = 3124342 };
        //modelBuilder.Entity<Autor>().HasData(new Autor())
        

    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=54321;Database=postgres-buechereien-rel;Username=postgres;Password=password1234");
    }
}
