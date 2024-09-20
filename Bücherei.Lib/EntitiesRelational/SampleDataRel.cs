using Bücherei.Lib.Services;
using Rel = Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.EntitiesRelational;

public class SampleDataRel
{
    public BuechereiRel[] buechereiRels;
    public Autor[] autorenRel;
    public Buch[] buecherRel;

    public Dictionary<int, List<int>> authorBooksIds = new();
    public Dictionary<int, List<int>> libAuthorsIds = new();

    public SampleDataRel (SampleData sampleData)
    {
        var relBuechereien = new List<Rel.BuechereiRel>();
        var relAutoren = new List<Rel.Autor>();
        var relBuecher = new List<Rel.Buch>();

        this.authorBooksIds = sampleData.authorBooksIds;
        this.libAuthorsIds = sampleData.libAuthorsIds;

        // make all libs
        foreach (SampleData.Buecherei lib in sampleData.Buechereien)
        {
            var relLib = new Rel.BuechereiRel()
            {
                BuechereiId = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Rel.Autor>()
            };
            relBuechereien.Add(relLib);
        }
        this.buechereiRels = relBuechereien.ToArray();

        // make all authors
        foreach (SampleData.Autor aut in sampleData.Autoren)
        {
            var relAutor = new Rel.Autor()
            {
                AutorId = aut.Id,
                Vorname = aut.Firstname,
                Nachname = aut.Surname,
                Buecher = Array.Empty<Rel.Buch>(),
                Buechereien = Array.Empty<Rel.BuechereiRel>()
            };
            relAutoren.Add(relAutor);
        }
        this.autorenRel = relAutoren.ToArray();

        // make all relBooks
        foreach (SampleData.Buch buch in sampleData.Buecher)
        {
            var relBuch = new Rel.Buch()
            {
                BuchId = buch.Id,
                Titel = buch.Title,
                AutorId = buch.AutorId
            };
            relBuecher.Add(relBuch);
        }
        this.buecherRel = relBuecher.ToArray();
    }
}