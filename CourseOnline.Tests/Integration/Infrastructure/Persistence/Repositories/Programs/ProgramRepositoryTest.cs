using CourseOnline.Application.Factories;
using CourseOnline.Application.Programs.DTOs.Inputs;
using CourseOnline.Infrastructure.Persistence.EFC.Repositories.Programs;

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
}
