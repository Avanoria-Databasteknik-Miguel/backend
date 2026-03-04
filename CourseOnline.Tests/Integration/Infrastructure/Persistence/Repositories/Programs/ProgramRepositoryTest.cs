using CourseOnline.Application.Factories;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;
using CourseOnline.Infrastructure.Persistence.EFC.UnitOfWork;

namespace CourseOnline.Tests.Integration.Infrastructure.Persistence.Repositories.Programs;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class ProgramRepositoryTest(SqliteInMemoryFixture fixture)
{
    [Fact]
    public async Task CreateAsync_ShouldCreateProgram_ReturnTrue()
    {
        CancellationToken ct = CancellationToken.None;
        //context = database
        await using var context = fixture.CreateContext();

        CreateProgramInput input = new("WebbUtvecklare .NET", null, null);

        var program = ProgramFactory.Create(input);


        //repo
        var systemUnderTest = new ProgramRepository(context);



        var created = await systemUnderTest.AddASync(program, ct);

        await context.SaveChangesAsync(ct);

        var fromDb = await systemUnderTest.GetByIdAsync(created.Id, ct);


        Assert.NotNull(fromDb);
        Assert.Equal(input.Name.Trim().ToLowerInvariant(), fromDb!.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProgram_ModifiedAtChanges()
    {
        var ct = CancellationToken.None;

        await using var context = fixture.CreateContext();
        var repo = new ProgramRepository(context);
        var uow = new EfcUnitOfWork(context);

        // Arrange: skapa program
        var input = new CreateProgramInput($"WebbUtvecklare .NET - {nameof(UpdateAsync_ShouldUpdateProgram_ModifiedAtChanges)} - {Guid.NewGuid()}", 10, 20);
        var program = ProgramFactory.Create(input);

        var created = await repo.AddASync(program, ct);
        await uow.SaveChangesAsync(ct);

        var before = await repo.GetOutputByIdAsync(created.Id, ct);
        Assert.NotNull(before);

        // Act: uppdatera
        created.Update("WebbUtvecklare .NET (uppdaterad)", 15, 25);

        var updated = await repo.UpdateAsync(created.Id, created, ct);
        Assert.NotNull(updated);

        // Liten delay så ModifiedAt med säkerhet hinner ändras (kan vara för snabb annars)
        await Task.Delay(20, ct);

        await uow.SaveChangesAsync(ct);

        var after = await repo.GetOutputByIdAsync(created.Id, ct);
        Assert.NotNull(after);

        // Assert: data ändras + CreatedAt samma + ModifiedAt ändras
        Assert.Equal("webbutvecklare .net (uppdaterad)", after!.Name);
        Assert.Equal(15, after.DurationWeeks);
        Assert.Equal(25, after.MaxStudents);

        Assert.Equal(before!.CreatedAtUtc, after.CreatedAtUtc);
        Assert.True(after.ModifiedAtUtc > before.ModifiedAtUtc);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnNull_WhenNotFound()
    {
        var ct = CancellationToken.None;

        await using var context = fixture.CreateContext();
        var repo = new ProgramRepository(context);

        var result = await repo.GetByNameAsync("does-not-exist", ct);

        Assert.Null(result);
    }
}
