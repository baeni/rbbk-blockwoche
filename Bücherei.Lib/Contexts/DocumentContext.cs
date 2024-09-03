using Bücherei.Lib.EntitiesDocument;
using Microsoft.EntityFrameworkCore;

namespace Bücherei.Lib.Contexts;

public class DocumentContext : DbContext
{
    public DbSet<BüchereiDoc> BüchereiDocs {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BüchereiDoc>()
            .OwnsMany(b => b.Autoren, c =>
            {
                c.ToJson();
                c.OwnsMany(d => d.Bücher);
            });
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=54322;Database=postgres-buecherdocs;Username=postgres;Password=password1234");
    }
}
