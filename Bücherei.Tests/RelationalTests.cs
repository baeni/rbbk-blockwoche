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
}