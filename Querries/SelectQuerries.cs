using Bücherei.Lib.Services;
using Npgsql;
using System.Diagnostics;
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

            SampleDataService sampleDataService = new SampleDataService(
                connectionString: "Host=localhost;Port=54321;Database=postgres;Username=postgres;Password=password123",
                sampleDataJsonPath: "../../../../Bücherei.Cli/sample-data.json");

            await sampleDataService.RecreateTablesAsync();
            await sampleDataService.FillTablesWithSampleDataAsync();
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