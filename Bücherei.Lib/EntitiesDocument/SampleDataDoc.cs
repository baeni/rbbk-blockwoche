using Bücherei.Lib.Services;
using Doc = Bücherei.Lib.EntitiesDocument;

namespace Bücherei.Lib.EntitiesDocument;

public class SampleDataDoc
{
    public BuechereiDoc[] BuechereienDoc;
    public Autor[] AutorenDoc;
    public Buch[] BuecherDoc;

    // Die Id's der Bücher, die einem Autor zugehörig sind
    public Dictionary<int, List<int>> AutorBuchIds = new();
    
    // Die Id's der Autoren, die einer Bücherei zugehörig sind
    public Dictionary<int, List<int>> BuechereiAutorIds = new();

    public SampleDataDoc(SampleData sampleData)
    {
        var buechereienDoc = new List<Doc.BuechereiDoc>();
        var autorenDoc = new List<Doc.Autor>();
        var buecherDoc = new List<Doc.Buch>();

        this.AutorBuchIds = sampleData.AutorBuchIds;
        this.BuechereiAutorIds = sampleData.BuechereiAutorIds;

        // make all Büchereien
        foreach (SampleData.Buecherei lib in sampleData.Buechereien)
        {
            var relLib = new Doc.BuechereiDoc()
            {
                Id = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Doc.Autor>()
            };
            buechereienDoc.Add(relLib);
        }
        this.BuechereienDoc = buechereienDoc.ToArray();

        // make all Autoren
        foreach (SampleData.Autor aut in sampleData.Autoren)
        {
            var relAutor = new Doc.Autor()
            {
                Id = aut.Id,
                Vorname = aut.Vorname,
                Nachname = aut.Nachname,
                Buecher = Array.Empty<Doc.Buch>()
            };
            autorenDoc.Add(relAutor);
        }
        this.AutorenDoc = autorenDoc.ToArray();

        // make all Bücher
        foreach (SampleData.Buch buch in sampleData.Buecher)
        {
            var relBuch = new Doc.Buch()
            {
                Id = buch.Id,
                Titel = buch.Title
            };
            buecherDoc.Add(relBuch);
        }
        this.BuecherDoc = buecherDoc.ToArray();

        // assign Bücher to Autoren
        foreach (Doc.Autor aut in autorenDoc)
        {
            var bookIndexes = AutorBuchIds[aut.Id];
            var authBooks = new List<Doc.Buch>();
            foreach (int bIndex in bookIndexes) {
                authBooks.Add(buecherDoc[bIndex -1]);
            }
            aut.Buecher = authBooks.ToArray();
        }

        // assign Autoren to Büchereien
        foreach (Doc.BuechereiDoc lib in buechereienDoc)
        {
            var authorIndexes = BuechereiAutorIds[lib.Id];
            var libAuthors = new List<Doc.Autor>();
            foreach (int aIndex in authorIndexes)
            {
                libAuthors.Add(autorenDoc[aIndex -1]);
            }
            lib.Autoren = libAuthors.ToArray();
        }
    }
}