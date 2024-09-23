using Bücherei.Lib.Contexts;
using Bücherei.Lib.Services;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Bücherei.Tests;

public class RelationalTests : IClassFixture<OutputDataFixture>
{
    private OutputDataFixture _outputDataFixture;
    private TestOutputData _outputDataRel;

    private string sdSmallPath = "sampledata/sample-data-small.json";
    private string sdMediumPath = "sampledata/sample-data-medium.json";
    private string sdLargePath = "sampledata/sample-data-large.json";

    private readonly RelationalContext _context;
    private readonly ITestOutputHelper _output;

    public RelationalTests(OutputDataFixture fixture ,ITestOutputHelper helper)
    {
        _outputDataFixture = fixture;
        _outputDataRel = fixture._outputDataRel;
        _context = new RelationalContext();
        _output = helper;
    }


    // CREATE TESTS:
    [Fact]
    public async Task Create_5Libraries_50Authors_500Books()
    {
        var data = SampleData.GetRel(sdSmallPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienRel);
        _context.Autoren.AddRange(data.AutorenRel);
        _context.Buecher.AddRange(data.BuecherRel);
        _context.AddRange(data.AutorBuechereiJunctions);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataRel.CreateSmall.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Create_50Libraries_500Authors_5000Books()
    {
        var data = SampleData.GetRel(sdMediumPath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienRel);
        _context.Autoren.AddRange(data.AutorenRel);
        _context.Buecher.AddRange(data.BuecherRel);
        _context.AddRange(data.AutorBuechereiJunctions);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataRel.CreateMedium.Add(sw.Elapsed);
    }

    [Fact]
    public async Task Create_500Libraries_5000Authors_50000Books()
    {
        var data = SampleData.GetRel(sdLargePath);
        Stopwatch sw = Stopwatch.StartNew();
        sw.Start();
        _context.Database.BeginTransaction();
        _context.Buechereien.AddRange(data.BuechereienRel);
        _context.Autoren.AddRange(data.AutorenRel);
        _context.Buecher.AddRange(data.BuecherRel);
        _context.AddRange(data.AutorBuechereiJunctions);

        await _context.Database.RollbackTransactionAsync();

        sw.Stop();
        _output.WriteLine(sw.Elapsed.ToString());
        this._outputDataRel.CreateLarge.Add(sw.Elapsed);
    }

    //SEARCH TESTS:





}