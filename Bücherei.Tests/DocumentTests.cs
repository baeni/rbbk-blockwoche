using Bücherei.Lib.Contexts;
using Xunit.Abstractions;

namespace Bücherei.Tests;

public class DocumentTests
{
    private readonly DocumentContext _context;
    private readonly ITestOutputHelper _output;

    public DocumentTests(ITestOutputHelper helper)
    {
        _context = new DocumentContext();
        _output = helper;
    }
}