using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using Xunit;

namespace KeelteKoolV2.xUnitTesting;

public class StudentServicesTests : TestBase
{
    [Fact]
    public async Task Should_CreateStudent_WhenResultIsReturned()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var result = await Svc<IStudentServices>().Create(dto);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_ReturnStudentDetails_WhenIdIsProvided()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<IStudentServices>().Create(dto);
        var result = await Svc<IStudentServices>().DetailAsync(created.Id);

        //kontroll
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
    }

    [Fact]
    public async Task Should_DeleteStudent_WhenIdIsProvided()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<IStudentServices>().Create(dto);
        var details = await Svc<IStudentServices>().DetailAsync(created.Id);
        var result = await Svc<IStudentServices>().DeleteAsync(created.Id);
        var details2 = await Svc<IStudentServices>().DetailAsync(created.Id);

        //kontroll
        Assert.NotNull(details);
        Assert.Equal(details.Id, created.Id);
        Assert.NotNull(result);
        Assert.Null(details2);
    }

    [Fact]
    public async Task Should_UpdateStudentFirstName_WhenNewDataIsProvided()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<IStudentServices>().Create(dto);
        var oldName = created.FirstName;
        
        dto.FirstName = "Uusnimi";
        var result = await Svc<IStudentServices>().UpdateAsync(created.Id, dto);

        //kontroll
        Assert.NotNull(result);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.NotEqual(oldName, result.FirstName);
    }

    private StudentDTO CreateDefaultDTO()
    {
        return new StudentDTO
        {
            FirstName = "Nikita",
            LastName = "Pahapoiss",
            Grades = [ "MA", "X", "2" ]
        };
    }
}
