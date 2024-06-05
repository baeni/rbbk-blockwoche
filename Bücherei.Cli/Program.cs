using System.Text.Json;
using Bücherei.Cli;
using Npgsql;

var connString = "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123";
var dataSource = NpgsqlDataSource.Create(connString);

var json = await File.ReadAllTextAsync("./sample-data.json");
var data = JsonSerializer.Deserialize<Data>(json);

// Delete tables
await using var dropTablesCmd = dataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren, buchobjekte");
await dropTablesCmd.ExecuteNonQueryAsync();

// Create tables
await using var createTablesCmd = dataSource.CreateCommand(
    "CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);" +
    "CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);" +
    "CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, titel varchar, autorId int REFERENCES autoren(id), büchereiId int REFERENCES büchereien(id));");
await createTablesCmd.ExecuteNonQueryAsync();

// Insert into autoren
foreach (var autor in data.Autoren)
{
    await using var cmd = dataSource.CreateCommand("INSERT INTO autoren (id, vorname, nachname) VALUES (@id, @vorname, @nachname)");
    cmd.Parameters.AddWithValue("id", autor.Id);
    cmd.Parameters.AddWithValue("vorname", autor.Vorname);
    cmd.Parameters.AddWithValue("nachname", autor.Nachname);
    await cmd.ExecuteNonQueryAsync();
}

// Insert into büchereien
foreach (var bücherei in data.Büchereien)
{
    await using var cmd = dataSource.CreateCommand("INSERT INTO büchereien (id, name) VALUES (@id, @name)");
    cmd.Parameters.AddWithValue("id", bücherei.Id);
    cmd.Parameters.AddWithValue("name", bücherei.Name);
    await cmd.ExecuteNonQueryAsync();
}

// Insert into bücher
foreach (var buch in data.Bücher)
{
    await using var cmd = dataSource.CreateCommand("INSERT INTO bücher (id, titel, autorId, büchereiId) VALUES (@id, @titel, @autorId, @büchereiId)");
    cmd.Parameters.AddWithValue("id", buch.Id);
    cmd.Parameters.AddWithValue("titel", buch.Titel);
    cmd.Parameters.AddWithValue("autorId", buch.AutorId);
    cmd.Parameters.AddWithValue("büchereiId", buch.BüchereiId);
    await cmd.ExecuteNonQueryAsync();
}