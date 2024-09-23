using Bücherei.Lib.Services;
using Rel = Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.EntitiesRelational;

public class SampleDataRel
{
    public List<BuechereiRel> BuechereienRel;
    public List<Autor> AutorenRel;
    public List<Buch> BuecherRel;
    public List<Rel.AutorBuecherei> AutorBuechereiJunctions;
    
    // Die Id's der Bücher, die einem Autor zugehörig sind
    public Dictionary<int, List<int>> AutorBuchIds = new();
    
    // Die Id's der Autoren, die einer Bücherei zugehörig sind
    public Dictionary<int, List<int>> BuechereiAutorIds = new();


    public SampleDataRel (SampleData sampleData)
    {
        var buechereienRel = new List<Rel.BuechereiRel>();
        var autorenRel = new List<Rel.Autor>();
        var buecherRel = new List<Rel.Buch>();

        this.AutorBuchIds = sampleData.AutorBuchIds;
        this.BuechereiAutorIds = sampleData.BuechereiAutorIds;

        // make all Büchereien
        foreach (SampleData.Buecherei lib in sampleData.Buechereien)
        {
            var relLib = new Rel.BuechereiRel()
            {
                BuechereiId = lib.Id,
                Name = lib.Name,
                Autoren = new List<Rel.Autor>()
            };
            buechereienRel.Add(relLib);
        }
        this.BuechereienRel = buechereienRel;

        // make all Autoren
        foreach (SampleData.Autor aut in sampleData.Autoren)
        {
            var relAutor = new Rel.Autor()
            {
                AutorId = aut.Id,
                Vorname = aut.Vorname,
                Nachname = aut.Nachname,
                Buecher = new List<Rel.Buch>(),
                Buechereien = new List<Rel.BuechereiRel>()
            };
            autorenRel.Add(relAutor);
        }
        this.AutorenRel = autorenRel;

        // make all Bücher
        foreach (SampleData.Buch buch in sampleData.Buecher)
        {
            var relBuch = new Rel.Buch()
            {
                BuchId = buch.Id,
                Titel = buch.Title,
                AutorId = buch.AutorId
            };
            buecherRel.Add(relBuch);
        }
        this.BuecherRel = buecherRel;
        
        // create n:m relation object
        List<Rel.AutorBuecherei> autorBuechereiJunctions = new();

        for (int i = 1; i <= buechereienRel.Count; i++)
        {
            var indexes = BuechereiAutorIds[i];

            for (int j = 0; j < indexes.Count; j++)
            {
                autorBuechereiJunctions.Add(new Rel.AutorBuecherei { AutorId = indexes[j], BuechereiId = i } );
            }
        }

        AutorBuechereiJunctions = autorBuechereiJunctions;
    }
}