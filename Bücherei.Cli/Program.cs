using Npgsql;

var connString = "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123";
var dataSource = NpgsqlDataSource.Create(connString);



// Drop all tables
await using var dropTablesCmd = dataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren");
await dropTablesCmd.ExecuteNonQueryAsync();



// Create table Autoren
await using var autorenCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);");
await autorenCreateCmd.ExecuteNonQueryAsync();
// Insert into table Autoren
await using var autorenInsertCmd = dataSource.CreateCommand("INSERT INTO autoren (vorname, nachname) VALUES ('John', 'Doe')");
await autorenInsertCmd.ExecuteNonQueryAsync();



// Create table Büchereien
await using var büchereienCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);");
await büchereienCreateCmd.ExecuteNonQueryAsync();
// Insert into table Büchereien
await using var büchereienInsertCmd = dataSource.CreateCommand("INSERT INTO büchereien (name) VALUES ('Bibel')");
await büchereienInsertCmd.ExecuteNonQueryAsync();



// Create table Bücher
await using var bücherCreateCmd = dataSource.CreateCommand("CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, büchereiId int REFERENCES büchereien(id), titel varchar, autorId int REFERENCES autoren(id));");
await bücherCreateCmd.ExecuteNonQueryAsync();
// Insert into table Bücher
await using var bücherInsertCmd = dataSource.CreateCommand("INSERT INTO bücher (büchereiId, titel, autorId) VALUES (1, 'Stadtbücherei Lorem', 1)");
await bücherInsertCmd.ExecuteNonQueryAsync();