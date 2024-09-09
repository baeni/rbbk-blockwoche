using System.Diagnostics;
using Bücherei.Lib.Contexts;
using Bücherei.Lib.EntitiesRelational;
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
    public async Task CreateBuecherei()
    {
        var watch = new Stopwatch();
        watch.Start();
        await CreateBuechereiAsync();
        watch.Stop();
        _output.WriteLine($"The Db operation took {watch.ElapsedMilliseconds}ms");
    }

    private async Task CreateBuechereiAsync()
    {
        var buecherei = new BuechereiRel();
        buecherei.Name = "Benny's Buecherei";
        //buecherei.Autoren = new Autor[] { new Autor(), new Autor() };

        _context.Buechereien.Add(buecherei);
        await _context.SaveChangesAsync();
    }
}