using System.Diagnostics;
using Bücherei.Lib.Contexts;
using Xunit.Abstractions;

namespace Bücherei.Tests;

public class RelationalTests
{
    private readonly RelationalContext _context;
    private readonly ITestOutputHelper _output;

    public RelationalTests(ITestOutputHelper helper)
    {
        _context = new RelationalContext();
        _output = helper;
    }

    //X
    [Fact]
    public async Task GetAllBookDataX()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = await GetAllBookDataAsyncX();
        watch.Stop();
        _output.WriteLine($"The Db operation took {watch.ElapsedMilliseconds}ms");
    }

    private async Task<ICollection<object>> GetAllBookDataAsyncX()
    {
        await using var bücherSelectCmd = _dataSource!.CreateCommand(
            "SELECT * " +
            "FROM bücher " +
            "WHERE Titel = 'Max und Moritz';"
        );

        var data = new List<object>();

        await using var reader = await bücherSelectCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            data.Add(reader.GetInt32(0));
        }

        return data;
    }

    //3
    [Fact]
    public async Task GetAllBookData3()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = await GetAllBookDataAsync3();
        watch.Stop();
        _output.WriteLine($"The Db operation took {watch.ElapsedMilliseconds}ms");
    }

    private async Task<ICollection<object>> GetAllBookDataAsync3()
    {
        await using var bücherSelectCmd = _dataSource!.CreateCommand(
            "SELECT * " +
            "FROM bücher " +
            "WHERE Titel = 'Max und Moritz';"
        );

        var data = new List<object>();

        await using var reader = await bücherSelectCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            data.Add(reader.GetInt32(0));
        }

        return data;
    }

    //1
    [Fact]
    public async Task GetAllBookData()
    {
        var watch = new Stopwatch();
        watch.Start();
        var data = await GetAllBookDataAsync();
        watch.Stop();
        _output.WriteLine($"The Db operation took {watch.ElapsedMilliseconds}ms");
    }

    private async Task<ICollection<object>> GetAllBookDataAsync()
    {
        await using var bücherSelectCmd = _dataSource!.CreateCommand(
            "SELECT * " +
            "FROM bücher " +
            "WHERE Titel = 'Max und Moritz';"
        );

        var data = new List<object>();

        await using var reader = await bücherSelectCmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            data.Add(reader.GetInt32(0));
        }

        return data;
    }
}