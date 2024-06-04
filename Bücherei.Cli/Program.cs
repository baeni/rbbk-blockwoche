using System.Text.Json;
using Bücherei.Lib.Entities;
using Npgsql;

var connString = "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123";
var dataSource = NpgsqlDataSource.Create(connString);



// Drop all tables
await using var dropTablesCmd = dataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren, buchobjekte");
await dropTablesCmd.ExecuteNonQueryAsync();



// Create table Autoren
await using var autorenCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);");
await autorenCreateCmd.ExecuteNonQueryAsync();
// Insert into table Autoren
await using var autorenInsertCmd = dataSource.CreateCommand("INSERT INTO autoren (vorname, nachname) VALUES " +
                                                            "('Wilhelm', 'Busch'), " +
                                                            "('Frank', 'Herbert'), " +
                                                            "('Unbekannt', 'Unbekannt'), " +
                                                            "('Astrid', 'Lindgren'), " +
                                                            "('Walter', 'Moers')");
await autorenInsertCmd.ExecuteNonQueryAsync();



// Create table Büchereien
await using var büchereienCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);");
await büchereienCreateCmd.ExecuteNonQueryAsync();
// Insert into table Büchereien
await using var büchereienInsertCmd = dataSource.CreateCommand("INSERT INTO büchereien (name) VALUES " +
                                                               "('Staatsbibliothek zu Berlin'), " +
                                                               "('Universitäts Johann Christian Senckenberg')");
await büchereienInsertCmd.ExecuteNonQueryAsync();



// Create table Bücher
await using var bücherCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, titel varchar, autorId int REFERENCES autoren(id), büchereiId int REFERENCES büchereien(id));");
await bücherCreateCmd.ExecuteNonQueryAsync();
// Insert into table Bücher
await using var bücherInsertCmd = dataSource.CreateCommand("INSERT INTO bücher (titel, autorId, büchereiId) VALUES " +
                                                           "('Max und Moritz', 1, 1), " +
                                                           "('Der Wüstenplanet', 2, 1), " +
                                                           "('Die Bibel', 3, 2), " +
                                                           "('Pipi Langstrumpf', 4, 1), " +
                                                           "('Die Stadt der Träumenden Bücher', 5, 2)");
await bücherInsertCmd.ExecuteNonQueryAsync();



// Create table Buchobjekte
await using var buchobjekteCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS buchobjekte (id SERIAL PRIMARY KEY, buchobjekt JSONB);");
await buchobjekteCreateCmd.ExecuteNonQueryAsync();
// Insert into table Buchobjekte
var autor = new Autor { Id = 1, Vorname = "Wilhelm", Nachname = "Busch" };
var bücherei = new Bücherei.Lib.Entities.Bücherei { Id = 1, Name = "Staatsbibliothek zu Berlin" };
var buch = new Buch
{
    Id = 1,
    Titel = "Max und Moritz",
    Autor = autor,
    Bücherei = bücherei
};
await using var buchobjekteInsertCmd = dataSource.CreateCommand("INSERT INTO buchobjekte (buchobjekt) VALUES " +
                                                           $"('{JsonSerializer.Serialize(buch)}')");
await buchobjekteInsertCmd.ExecuteNonQueryAsync();