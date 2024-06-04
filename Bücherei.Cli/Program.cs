using Npgsql;

var connString = "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123";
var conn = new NpgsqlConnection(connString);

conn.Open();

// Autoren
var autorenCreateCmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);", conn);
var autorenInsertCmd = new NpgsqlCommand("INSERT INTO autoren (vorname, nachname) VALUES ('John', 'Doe')", conn);
if (autorenCreateCmd.ExecuteNonQuery() > 0)
    autorenInsertCmd.ExecuteNonQuery();

// Büchereien
var büchereienCreateCmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);", conn);
var büchereienInsertCmd = new NpgsqlCommand("INSERT INTO büchereien (name) VALUES ('Bibel')", conn);
if (büchereienCreateCmd.ExecuteNonQuery() > 0)
    büchereienInsertCmd.ExecuteNonQuery();

// Bücher
var bücherCreateCmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, büchereiId int REFERENCES büchereien(id), titel varchar, autorId int REFERENCES autoren(id));", conn);
var bücherInsertCmd = new NpgsqlCommand("INSERT INTO bücher (büchereiId, titel, autorId) VALUES (1, 'Stadtbücherei Lorem', 1)", conn);
if (bücherCreateCmd.ExecuteNonQuery() > 0)
    bücherInsertCmd.ExecuteNonQuery();

conn.Close();