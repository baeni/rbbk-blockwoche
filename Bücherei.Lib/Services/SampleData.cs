using System.Text.Json;

using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.EntitiesDocument;
using Bücherei.Lib.EntitiesRelational;

namespace Bücherei.Lib.Services;

public class SampleData
{
    private const string FILE_PATH = "./sample-data.json";
    
    public SampleData.Buecherei[] Buechereien { get; set; }
    public SampleData.Autor[] Autoren { get; set; }
    public SampleData.Buch[] Buecher { get; set; }

    // Die Id's der Bücher, die einem Autor zugehörig sind
    public Dictionary<int, List<int>> AutorBuchIds = new();
    
    // Die Id's der Autoren, die einer Bücherei zugehörig sind
    public Dictionary<int, List<int>> BuechereiAutorIds = new();

    public class Buecherei
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Autoren { get; set; }
    }

    public class Autor
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int[] Buecher { get; set; }
        public int[] BuechereiIds { get; set; }
    }

    public class Buch
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AutorId { get; set; }
    }

    public static SampleData GetRaw()
    {
        var sampleDataJson = File.ReadAllText(FILE_PATH);
        var sampleData = JsonSerializer.Deserialize<SampleData>(sampleDataJson);

        // fill AutorBuchIds
        foreach (SampleData.Buch buch in sampleData.Buecher)
        {
            if (!sampleData.AutorBuchIds.ContainsKey(buch.AutorId))
            {
                sampleData.AutorBuchIds.Add(buch.AutorId, new List<int>());
            }

            var books = sampleData.AutorBuchIds[buch.AutorId];
            books.Add(buch.Id);
        }

        // fill BuechereiAutorIds
        foreach (SampleData.Autor aut in sampleData.Autoren)
        {
            foreach (int buechereiId in aut.BuechereiIds)
            {
                if (!sampleData.BuechereiAutorIds.ContainsKey(buechereiId))
                {
                    sampleData.BuechereiAutorIds.Add(buechereiId, new List<int>());
                }

                var autIds = sampleData.BuechereiAutorIds[buechereiId];
                if (!autIds.Contains(aut.Id))
                    autIds.Add(aut.Id);
            }
        }

        return sampleData;
    }

    public static SampleDataRel GetRel()
    {
        var data = SampleData.GetRaw();
        var relData = new SampleDataRel(data);

        return relData;
    }

    public static SampleDataDoc GetDoc()
    {
        var data = SampleData.GetRaw();
        var relData = new SampleDataDoc(data);

        return relData;
    }
}