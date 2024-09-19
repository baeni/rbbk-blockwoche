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
using Microsoft.EntityFrameworkCore;

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
        var data = SampleData.GetRaw();
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
        _output.WriteLine("Start:");

        RelationalContext context = new RelationalContext();

        _output.WriteLine("Hurra 1 !");


        var builder = new ModelBuilder();
        builder.SeedRel();

        _output.WriteLine("Hurra 2 !");
    }
}