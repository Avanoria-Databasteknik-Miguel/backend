using CourseOnline.Infrastructure.Persistence.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CourseOnline.Tests.Integration.Infrastructure.Persistence;

public sealed class SqliteInMemoryFixture : IAsyncLifetime
{

    private SqliteConnection? _conn;
    public DbContextOptions<CourseOnlineDbContext> Options { get; private set; } = default!;
    public async Task DisposeAsync()
    {
        if (_conn is not null) await _conn.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        _conn = new SqliteConnection("Data Source=:memory:;Cache=Shared");
        await _conn.OpenAsync();

        Options = new DbContextOptionsBuilder<CourseOnlineDbContext>().UseSqlite(_conn).EnableSensitiveDataLogging().Options;

        await using var context = new CourseOnlineDbContext(Options);
        await context.Database.EnsureCreatedAsync();
    }

    public CourseOnlineDbContext CreateContext() => new(Options);
}

[CollectionDefinition(Name)]
public sealed class SqliteInMemoryCollection : ICollectionFixture<SqliteInMemoryFixture>
{
    public const string Name = "SwliteInMemory";
};