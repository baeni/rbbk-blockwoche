using Bücherei.Lib.Contexts;
using Bücherei.Lib.Services;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Bücherei.Tests;

public class DocumentTests : IClassFixture<OutputDataFixture>
{
    private OutputDataFixture _outputDataFixture;
    private TestOutputData _outputDataDoc;

    private string sdSmallPath = "sampledata/sample-data-small.json";
    private string sdMediumPath = "sampledata/sample-data-medium.json";
    private string sdLargePath = "sampledata/sample-data-large.json";

    private readonly DocumentContext _context;
    private readonly ITestOutputHelper _output;

    public DocumentTests(OutputDataFixture fixture, ITestOutputHelper helper)
    {
        _outputDataFixture = fixture;
        _outputDataDoc = fixture._outputDataDoc;
        _context = new DocumentContext();
        _output = helper;
    }



    // CREATE TESTS:
    [Fact]
    public async Task Create_5Libraries_50Authors_500Books()
    {
        var data = SampleData.GetDoc(sdSmallPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateSmall.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Create_50Libraries_500Authors_5000Books()
    {
        var data = SampleData.GetDoc(sdMediumPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateMedium.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Create_500Libraries_5000Authors_50000Books()
    {
        var data = SampleData.GetDoc(sdLargePath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateLarge.Add(sw.Elapsed);
    }

    //SEARCH TESTS:
    [Fact]
    public async Task Search_5Libraries_50Authors_500Books()
    {
        var data = SampleData.GetDoc(sdSmallPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        // hier noch nach ein paar Büchern suchen:
        // z.b Context. suche nach allen Büchern mit Buchstabe "A" im Titel.

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateSmall.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Search_50Libraries_500Authors_5000Books()
    {
        var data = SampleData.GetDoc(sdMediumPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        // hier noch nach ein paar Büchern suchen:
        // z.b Context. suche nach allen Büchern mit Buchstabe "A" im Titel.

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateMedium.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Search_500Libraries_5000Authors_50000Books()
    {
        var data = SampleData.GetDoc(sdLargePath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienDoc);

        // hier noch nach ein paar Büchern suchen:
        // z.b Context. suche nach allen Büchern mit Buchstabe "A" im Titel.

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataDoc.CreateLarge.Add(sw.Elapsed);
    }

}