using System.Diagnostics;
using Bücherei.Lib.Contexts;
using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.EntitiesDocument;
using Bücherei.Lib.EntitiesRelational;
using Xunit.Abstractions;
using Bücherei.Lib.Services;

namespace Bücherei.Tests;

public class TestDataGen(ITestOutputHelper helper)
{
    private readonly RelationalContext _context = new();

    private BuechereiRel[] buechereiRels;
    private Rel.Autor[] autorenRel;
    private Rel.Buch[] buecherRel;

    private BuechereiDoc[] buechereiDocs;
    private Doc.Autor[] autorenDoc;
    private Doc.Buch[] buecherDoc;

    //X
    [Fact]
    public async Task CreateBuecherei()
    {
        var watch = new Stopwatch();
        watch.Start();
        await CreateBuechereiAsync();
        watch.Stop();
        helper.WriteLine($"The Db operation took {watch.ElapsedMilliseconds}ms");
    }

    private async Task CreateBuechereiAsync()
    {
        var buecherei = new BuechereiRel()
        {
            Name = "Benny's Buecherei"
        };

        _context.Buechereien.Add(buecherei);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public void CreateRelBooks()
    {
        var data = SampleData.GetRaw();

        var newBuechereien = new List<Rel.BuechereiRel>();
        foreach (SampleData.Buecherei lib in data.Buechereien)
        {
            var newLib = new Rel.BuechereiRel()
            {
                BuechereiId = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Rel.Autor>()
            };
            newBuechereien.Add(newLib);
        }
    }

    [Fact]
    public void CreateContextRel()
    {
        RelationalContext context = new RelationalContext();
    }

    [Fact]
    public void CreateContextDoc()
    {
        DocumentContext context = new DocumentContext();
    }
}