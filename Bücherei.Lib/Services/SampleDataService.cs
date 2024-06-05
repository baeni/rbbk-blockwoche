using System.Text.Json;
using Bücherei.Lib.Entities;
using Npgsql;

namespace Bücherei.Lib.Services;

public class SampleDataService
{
    private readonly NpgsqlDataSource _dataSource = null!;
    
    private readonly SampleData? _sampleData;

    public SampleDataService(string connectionString, string sampleDataJsonPath)
    {
        _dataSource = NpgsqlDataSource.Create(connectionString);
        var sampleDataJson = File.ReadAllText(sampleDataJsonPath);
        _sampleData = JsonSerializer.Deserialize<SampleData>(sampleDataJson);
        
        ArgumentNullException.ThrowIfNull(_sampleData, $"sample-data.json could not be deserialized to {nameof(SampleData)}.");
    }
    
    /// <summary>
    /// Deletes all tables and recreates them afterwards.
    /// </summary>
    public async Task RecreateTablesAsync()
    {
        await DeleteTablesAsync();
        await CreateTablesAsync();
    }

    /// <summary>
    /// Fills all tables with sample data from <see href="sample-data.json"/>.
    /// </summary>
    public async Task FillTablesWithSampleDataAsync()
    {
        await InsertAutorenSampleDataAsync();
        await InsertBüchereienSampleDataAsync();
        await InsertBücherSampleDataAsync();
        await InsertBücherobjekteSampleDataAsync();
    }

    private async Task DeleteTablesAsync()
    {
        await using var dropTablesCmd = _dataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren, buchobjekte");
        await dropTablesCmd.ExecuteNonQueryAsync();
    }

    private async Task CreateTablesAsync()
    {
        await using var createTablesCmd = _dataSource.CreateCommand(
            "CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);" +
            "CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);" +
            "CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, titel varchar, autorId int REFERENCES autoren(id), büchereiId int REFERENCES büchereien(id));" +
            "CREATE TABLE IF NOT EXISTS bücherobjekte (buch JSONB);");
        await createTablesCmd.ExecuteNonQueryAsync();
    }

    private async Task InsertAutorenSampleDataAsync()
    {
        foreach (var autor in _sampleData!.Autoren)
        {
            await using var cmd = _dataSource.CreateCommand("INSERT INTO autoren (id, vorname, nachname) VALUES (@id, @vorname, @nachname)");
            cmd.Parameters.AddWithValue("id", autor.Id);
            cmd.Parameters.AddWithValue("vorname", autor.Vorname);
            cmd.Parameters.AddWithValue("nachname", autor.Nachname);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private async Task InsertBüchereienSampleDataAsync()
    {
        foreach (var bücherei in _sampleData!.Büchereien)
        {
            await using var cmd = _dataSource.CreateCommand("INSERT INTO büchereien (id, name) VALUES (@id, @name)");
            cmd.Parameters.AddWithValue("id", bücherei.Id);
            cmd.Parameters.AddWithValue("name", bücherei.Name);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private async Task InsertBücherSampleDataAsync()
    {
        foreach (var buch in _sampleData!.Bücher)
        {
            await using var cmd = _dataSource.CreateCommand("INSERT INTO bücher (id, titel, autorId, büchereiId) VALUES (@id, @titel, @autorId, @büchereiId)");
            cmd.Parameters.AddWithValue("id", buch.Id);
            cmd.Parameters.AddWithValue("titel", buch.Titel);
            cmd.Parameters.AddWithValue("autorId", buch.AutorId);
            cmd.Parameters.AddWithValue("büchereiId", buch.BüchereiId);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private async Task InsertBücherobjekteSampleDataAsync()
    {
        foreach (var buch in _sampleData!.Bücher)
        {
            await using var cmd = _dataSource.CreateCommand($"INSERT INTO bücherobjekte (buch) VALUES ('{JsonSerializer.Serialize(buch)}')");
            await cmd.ExecuteNonQueryAsync();
        }
    }
}