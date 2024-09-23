using Bücherei.Lib.Contexts;

namespace Bücherei.Tests;

class RelationalTestService
{
    private readonly RelationalContext _dbContext;

    public RelationalTestService(RelationalContext dbContext)
    {
        _dbContext = dbContext;
    }
}
