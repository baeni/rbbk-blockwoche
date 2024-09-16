using System.Text.Json;

using Npgsql;

using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.EntitiesDocument;
using Bücherei.Lib.EntitiesRelational;
using Microsoft.EntityFrameworkCore;
using static Bücherei.Lib.Services.SampleData;

namespace Bücherei.Lib.Services;

public class SampleData
{
    //private BuechereiRel[] buechereiRels;
    //private Rel.Autor[] autorenRel;
    //private Rel.Buch[] buecherRel;

    //private BuechereiDoc[] buechereiDocs;
    //private Doc.Autor[] autorenDoc;
    //private Doc.Buch[] buecherDoc;

    public SampleData.Buecherei[] Buechereien { get; set; }
    public SampleData.Autor[] Autoren { get; set; }
    public SampleData.Buch[] Buecher { get; set; }

    public class Buecherei
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Autoren { get; set; }
    }

    public class Autor
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        var sampleDataJson = File.ReadAllText("./sample-data-hybrid.json");
        var _sampleData = JsonSerializer.Deserialize<SampleData>(sampleDataJson);

        var sampleData = new SampleData();


        return sampleData;
    }

    public static SampleData.Buch GetBuch()
    {
        var sampleDataJson = File.ReadAllText("./sample-data-book.json");
        var book = JsonSerializer.Deserialize<SampleData.Buch>(sampleDataJson);


        return book;
    }

    public static SampleDataRel GetRel()
    {
        var data = SampleData.GetRaw();

        var relData = new SampleDataRel();

        var newBuechereien = new List<Rel.BuechereiRel>();
        foreach (SampleData.Buecherei lib in data.Buechereien)
        {
            var newLib = new Rel.BuechereiRel()
            {
                Id = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Rel.Autor>()
            };
            newBuechereien.Add(newLib);
        }

        var newAutoren = new List<Rel.Autor>();
        foreach (SampleData.Autor aut in data.Autoren)
        {
            var buechereiIds = new List<Rel.BuechereiRel>();
            foreach (var buechereiId in aut.BuechereiIds)
            {
                var buecherei = new BuechereiRel();
                buechereiIds.Add()   
            }
            
            var newAutor = new Rel.Autor()
            {
                Id = aut.Id,
                Name = aut.Name,
                Buecher = Array.Empty<Rel.Buch>(),
                Buechereien = Array.Empty<Rel.BuechereiRel>()
            };
            newAutoren.Add(newAutor);
        }

        var newBuecher = new List<Rel.Buch>();
        foreach (SampleData.Buch buch in data.Buecher)
        {
            var newBuch = new Rel.Buch()
            {
                Autor = buch.Id
                b
            };
            newAutoren.Add(newAutor);
        }
    }


}

//public class SampleDataService
//{
//    private readonly NpgsqlDataSource _dataSource = null!;

//    private readonly SampleData? _sampleData;

//    public SampleDataService(string connectionString, string sampleDataJsonPath)
//    {
//        _dataSource = NpgsqlDataSource.Create(connectionString);
//        var sampleDataJson = File.ReadAllText(sampleDataJsonPath);
//        _sampleData = JsonSerializer.Deserialize<SampleData>(sampleDataJson);

//        ArgumentNullException.ThrowIfNull(_sampleData, $"sample-data.json could not be deserialized to {nameof(SampleData)}.");
//    }

//    /// <summary>
//    /// Deletes all tables and recreates them afterwards.
//    /// </summary>
//    public async Task RecreateTablesAsync()
//    {
//        await DeleteTablesAsync();
//        await CreateTablesAsync();
//    }

//    /// <summary>
//    /// Fills all tables with sample data from <see href="sample-data.json"/>.
//    /// </summary>
//    public async Task FillTablesWithSampleDataAsync()
//    {
//        await InsertAutorenSampleDataAsync();
//        await InsertBüchereienSampleDataAsync();
//        await InsertBücherSampleDataAsync();
//        await InsertBücherobjekteSampleDataAsync();
//    }

//    private async Task DeleteTablesAsync()
//    {
//        await using var dropTablesCmd = _dataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren, buchobjekte");
//        await dropTablesCmd.ExecuteNonQueryAsync();
//    }

//    private async Task CreateTablesAsync()
//    {
//        await using var createTablesCmd = _dataSource.CreateCommand(
//            "CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);" +
//            "CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);" +
//            "CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, titel varchar, autorId int REFERENCES autoren(id), büchereiId int REFERENCES büchereien(id));" +
//            "CREATE TABLE IF NOT EXISTS bücherobjekte (buch JSONB);");
//        await createTablesCmd.ExecuteNonQueryAsync();
//    }

//    private async Task InsertAutorenSampleDataAsync()
//    {
//        foreach (var autor in _sampleData!.Autoren)
//        {
//            await using var cmd = _dataSource.CreateCommand("INSERT INTO autoren (id, vorname, nachname) VALUES (@id, @vorname, @nachname)");
//            cmd.Parameters.AddWithValue("id", autor.Id);
//            cmd.Parameters.AddWithValue("vorname", autor.Vorname);
//            cmd.Parameters.AddWithValue("nachname", autor.Nachname);
//            await cmd.ExecuteNonQueryAsync();
//        }
//    }

//    private async Task InsertBüchereienSampleDataAsync()
//    {
//        foreach (var bücherei in _sampleData!.Büchereien)
//        {
//            await using var cmd = _dataSource.CreateCommand("INSERT INTO büchereien (id, name) VALUES (@id, @name)");
//            cmd.Parameters.AddWithValue("id", bücherei.Id);
//            cmd.Parameters.AddWithValue("name", bücherei.Name);
//            await cmd.ExecuteNonQueryAsync();
//        }
//    }

//    private async Task InsertBücherSampleDataAsync()
//    {
//        foreach (var buch in _sampleData!.Buecher)
//        {
//            await using var cmd = _dataSource.CreateCommand("INSERT INTO bücher (id, titel, autorId, büchereiId) VALUES (@id, @titel, @autorId, @büchereiId)");
//            cmd.Parameters.AddWithValue("id", buch.Id);
//            cmd.Parameters.AddWithValue("titel", buch.Titel);
//            cmd.Parameters.AddWithValue("autorId", buch.AutorId);
//            await cmd.ExecuteNonQueryAsync();
//        }
//    }

//    private async Task InsertBücherobjekteSampleDataAsync()
//    {
//        foreach (var buch in _sampleData!.Buecher)
//        {
//            await using var cmd = _dataSource.CreateCommand($"INSERT INTO bücherobjekte (buch) VALUES ('{JsonSerializer.Serialize(buch)}')");
//            await cmd.ExecuteNonQueryAsync();
//        }
//    }
//}