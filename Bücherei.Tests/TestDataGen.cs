using System.Diagnostics;
using System.Net.NetworkInformation;
using Bücherei.Lib.Contexts;
using Doc = Bücherei.Lib.EntitiesDocument;
using Rel = Bücherei.Lib.EntitiesRelational;
using Bücherei.Lib.EntitiesDocument;
using Bücherei.Lib.EntitiesRelational;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Bücherei.Lib.Services;
using static Bücherei.Lib.Services.SampleData;

namespace Bücherei.Tests;

public class TestDataGen
{
    private readonly RelationalContext _context;
    private readonly ITestOutputHelper _output;

    private BuechereiRel[] buechereiRels;
    private Rel.Autor[] autorenRel;
    private Rel.Buch[] buecherRel;

    private BuechereiDoc[] buechereiDocs;
    private Doc.Autor[] autorenDoc;
    private Doc.Buch[] buecherDoc;

    public TestDataGen(ITestOutputHelper helper)
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
        var buecherei = new BuechereiRel()
        {
            Name = "Benny's Buecherei"
        };

        //buecherei.Autoren = new Autor[] { new Autor(), new Autor() };

        _context.Buechereien.Add(buecherei);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public void EmptyFact()
    {
        var data = SampleData.Get();
    }

    [Fact]
    public void EmptyFactBook()
    {
        var data = SampleData.GetBuch();
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
                Id = lib.Id,
                Name = lib.Name,
                Autoren = Array.Empty<Rel.Autor>()
            };
            newBuechereien.Add(newLib);
        }

        var newAutoren = new List<Rel.Autor>();
        foreach (SampleData.Autor aut in data.Autoren)
        {
            var newAutor = new Rel.Autor()
            {
                Id = aut.Id,
                Name = aut.Name,
                Buecher = Array.Empty<Rel.Buch>(),
                Buechereien = aut.BuechereiId

            };
            newAutoren.Add(newAutor);
        }

        var newBuecher = new List<Rel.Buch>();
        foreach (SampleData.Buch buch in data.Buecher)
        {
            var newBuch = new Rel.Buch()
            {
                Autor = buch.Id
                b
            };
            newAutoren.Add(newAutor);
        }

        foreach (SampleData.Buch buch in data.Buecher)
        {
            var newBuch = new Rel.Buch()
            {
                Autor = buch.Id
                b
            };
        }

        this.buecherRel




        _context.Buechereien.Add(buecherei);
        await _context.SaveChangesAsync();
    }
}