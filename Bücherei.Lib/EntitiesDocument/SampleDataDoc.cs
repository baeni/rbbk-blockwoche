using Bücherei.Lib.Services;
using Doc = Bücherei.Lib.EntitiesDocument;

namespace Bücherei.Lib.EntitiesDocument;

public class SampleDataDoc
{
    public BuechereiDoc[] buechereiDocs;
    public Autor[] autorenDoc;
    public Buch[] buecherDoc;

    public Dictionary<int, List<int>> authorBooksIds = new();
    public Dictionary<int, List<int>> libAuthorsIds = new();

    public SampleDataDoc(SampleData sampleData)
    {
        var docBuechereien = new List<Doc.BuechereiDoc>();
        var docAutoren = new List<Doc.Autor>();
        var docBuecher = new List<Doc.Buch>();

        this.authorBooksIds = sampleData.authorBooksIds;
        this.libAuthorsIds = sampleData.libAuthorsIds;

        // make all libs
        foreach (SampleData.Buecherei lib in sampleData.Buechereien)
        {
            var relLib = new Doc.BuechereiDoc()
            {
                Id = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Doc.Autor>()
            };
            docBuechereien.Add(relLib);
        }
        this.buechereiDocs = docBuechereien.ToArray();

        // make all authors
        foreach (SampleData.Autor aut in sampleData.Autoren)
        {
            var relAutor = new Doc.Autor()
            {
                Id = aut.Id,
                Vorname = aut.Firstname,
                Nachname = aut.Surname,
                Buecher = Array.Empty<Doc.Buch>()
            };
            docAutoren.Add(relAutor);
        }
        this.autorenDoc = docAutoren.ToArray();

        // make all books
        foreach (SampleData.Buch buch in sampleData.Buecher)
        {
            var relBuch = new Doc.Buch()
            {
                Id = buch.Id,
                Titel = buch.Title
            };
            docBuecher.Add(relBuch);
        }
        this.buecherDoc = docBuecher.ToArray();

        // assign books to authors
        foreach (Doc.Autor aut in docAutoren)
        {
            var bookIndexes = authorBooksIds[aut.Id];
            var authBooks = new List<Doc.Buch>();
            foreach (int bIndex in bookIndexes) {
                authBooks.Add(docBuecher[bIndex -1]);
            }
            aut.Buecher = authBooks.ToArray();
        }

        //assign authors to libs
        foreach (Doc.BuechereiDoc lib in docBuechereien)
        {
            var authorIndexes = libAuthorsIds[lib.Id];
            var libAuthors = new List<Doc.Autor>();
            foreach (int aIndex in authorIndexes)
            {
                libAuthors.Add(docAutoren[aIndex -1]);
            }
            lib.Autoren = libAuthors.ToArray();
        }
    }
}