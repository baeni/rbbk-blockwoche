using Microsoft.EntityFrameworkCore;
using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.Services;

public static class ModelBuilderExtensions
{
    public static void SeedRel(this ModelBuilder modelBuilder)
    {
        var relData = SampleData.GetRel();

        modelBuilder.Entity<Rel.Autor>().HasData(relData.autorenRel);
        modelBuilder.Entity<Rel.BuechereiRel>().HasData(relData.buechereiRels);
        modelBuilder.Entity<Rel.Buch>().HasData(relData.buecherRel);

        List<Rel.AutorBuecherei> autorenBuechereienRel = new();

        for (int i = 1; i <= relData.buechereiRels.Length; i++)
        {
            var indexes = relData.libAuthorsIds[i];

            for (int j = 0; j < indexes.Count; j++)
            {
                autorenBuechereienRel.Add(new Rel.AutorBuecherei { AutorId = indexes[j], BuechereiId = i } );
            }
        }

        modelBuilder.Entity<Rel.BuechereiRel>()
            .HasMany(u => u.Autoren)
            .WithMany(r => r.Buechereien)
            .UsingEntity<Rel.AutorBuecherei>(j => j.HasData(
                autorenBuechereienRel
            ));
    }

    // deprecated, seeding for json not supported
    public static void SeedDoc(this ModelBuilder modelBuilder)
    {
        var relData = SampleData.GetDoc();

        //modelBuilder.Entity<Doc.BuechereiDoc>().HasData(relData.buechereiDocs);

    }
}
