using Bücherei.Lib.Contexts;

namespace Bücherei.Tests;

class DocumentTestService
{
    private readonly DocumentContext _dbContext;

    public DocumentTestService(DocumentContext dbContext)
    {
        _dbContext = dbContext;
    }
}
