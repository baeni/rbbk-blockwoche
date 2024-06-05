using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using Bücherei.Lib.Entities;
using Microsoft.Extensions.Logging;
using Npgsql;
using Xunit.Abstractions;

namespace Querries
{
    public class SelectQuerries
    {
        private static string ConnectionString = "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123";
        private NpgsqlDataSource? DataSource;

        private readonly ITestOutputHelper _output;

        public SelectQuerries(ITestOutputHelper helper)
        {
            this._output = helper;

            CreateDatabaseSetup().Wait();
        }

        private async Task CreateDatabaseSetup()
        {
            DataSource = NpgsqlDataSource.Create(ConnectionString);

            // Drop all tables
            await using var dropTablesCmd = DataSource.CreateCommand("DROP TABLE IF EXISTS bücher, büchereien, autoren, buchobjekte");
            await dropTablesCmd.ExecuteNonQueryAsync();


            // Create table Autoren
            await using var autorenCreateCmd = DataSource.CreateCommand("CREATE TABLE IF NOT EXISTS autoren (id SERIAL PRIMARY KEY, vorname varchar, nachname varchar);");
            await autorenCreateCmd.ExecuteNonQueryAsync();
            // Insert into table Autoren
            await using var autorenInsertCmd = DataSource.CreateCommand("INSERT INTO autoren (vorname, nachname) VALUES " +
                                                                        "('Wilhelm', 'Busch'), " +
                                                                        "('Frank', 'Herbert'), " +
                                                                        "('Unbekannt', 'Unbekannt'), " +
                                                                        "('Astrid', 'Lindgren'), " +
                                                                        "('Walter', 'Moers')");
            await autorenInsertCmd.ExecuteNonQueryAsync();


            // Create table Büchereien
            await using var büchereienCreateCmd = DataSource.CreateCommand("CREATE TABLE IF NOT EXISTS büchereien (id SERIAL PRIMARY KEY, name varchar);");
            await büchereienCreateCmd.ExecuteNonQueryAsync();
            // Insert into table Büchereien
            await using var büchereienInsertCmd = DataSource.CreateCommand("INSERT INTO büchereien (name) VALUES " +
                                                                           "('Staatsbibliothek zu Berlin'), " +
                                                                           "('Universitäts Johann Christian Senckenberg')");
            await büchereienInsertCmd.ExecuteNonQueryAsync();


            // Create table Bücher
            await using var bücherCreateCmd = DataSource.CreateCommand("CREATE TABLE IF NOT EXISTS bücher (id SERIAL PRIMARY KEY, titel varchar, autorId int REFERENCES autoren(id), büchereiId int REFERENCES büchereien(id));");
            await bücherCreateCmd.ExecuteNonQueryAsync();
            // Insert into table Bücher
            await using var bücherInsertCmd = DataSource.CreateCommand("INSERT INTO bücher (titel, autorId, büchereiId) VALUES " +
                                                                       "('Max und Moritz', 1, 1), " +
                                                                       "('Der Wüstenplanet', 2, 1), " +
                                                                       "('Die Bibel', 3, 2), " +
                                                                       "('Pipi Langstrumpf', 4, 1), " +
                                                                       "('Die Stadt der Träumenden Bücher', 5, 2)");
            await bücherInsertCmd.ExecuteNonQueryAsync();



            // Create table Buchobjekte
            await using var buchobjekteCreateCmd = DataSource.CreateCommand("CREATE TABLE IF NOT EXISTS buchobjekte (id SERIAL PRIMARY KEY, buchobjekt JSONB);");
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
            await using var buchobjekteInsertCmd = DataSource.CreateCommand("INSERT INTO buchobjekte (buchobjekt) VALUES " +
                                                                       $"('{JsonSerializer.Serialize(buch)}')");
            await buchobjekteInsertCmd.ExecuteNonQueryAsync();
        }

        //X
        [Fact]
        public async Task GetAllBookDataX()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await GetAllBookDataAsyncX();
            sw.Stop();
            _output.WriteLine(sw.ElapsedMilliseconds.ToString());
        }

        private async Task GetAllBookDataAsyncX()
        {
            await using var bücherSelectCmd = DataSource!.CreateCommand(
                "SELECT * " +
                "FROM bücher " +
                "WHERE Titel = 'Max und Moritz';"
            );

            var list = new List<object>();

            await using (var reader = await bücherSelectCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(reader.GetInt32(0));
                }
            }
        }

        //3
        [Fact]
        public async Task GetAllBookData3()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await GetAllBookDataAsync3();
            sw.Stop();
            _output.WriteLine(sw.ElapsedMilliseconds.ToString());
        }

        private async Task GetAllBookDataAsync3()
        {
            await using var bücherSelectCmd = DataSource!.CreateCommand(
                "SELECT * " +
                "FROM bücher " +
                "WHERE Titel = 'Max und Moritz';"
            );

            var list = new List<object>();

            await using (var reader = await bücherSelectCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(reader.GetInt32(0));
                }

            }
        }


        [Fact]
        public async Task GetAllBookData()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await GetAllBookDataAsync();
            sw.Stop();
            _output.WriteLine(sw.ElapsedMilliseconds.ToString());
        }

        //1
        private async Task GetAllBookDataAsync()
        {
            await using var bücherSelectCmd = DataSource!.CreateCommand(
                "SELECT * " +
                "FROM bücher " +
                "WHERE Titel = 'Max und Moritz';"
            );

            var list = new List<object>();

            await using (var reader = await bücherSelectCmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    list.Add(reader.GetInt32(0));
                }
            }
        }



    }
}