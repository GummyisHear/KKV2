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

    /* ~~--====--~~ Lisa 5 testid ~~--====--~~ */

    [Fact]
    public async Task Should_UpdateStudentGrades_WhenNewDataIsProvided()
    {
        // ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<IStudentServices>().Create(dto);
        var oldGrades = created.Grades.ToList(); // kopeeri list

        dto.Grades[0] = "5";
        var result = await Svc<IStudentServices>().UpdateAsync(created.Id, dto);

        // kontroll
        Assert.NotNull(result);
        Assert.True(oldGrades.Count > 0);
        Assert.True(result.Grades.Count > 0);
        Assert.NotEqual(oldGrades[0], result.Grades[0]);
    }

    [Fact]
    public async Task Should_UseDefaultNames_WhenNamesAreEmpty()
    {
        // ülesseade
        var dto = CreateDefaultDTO();
        dto.FirstName = "";
        dto.LastName = "";

        // tegevus
        var result = await Svc<IStudentServices>().Create(dto);

        // kontroll
        Assert.Equal("John", result.FirstName);
        Assert.Equal("Doe", result.LastName);
    }

    [Fact]
    public async Task Should_ReturnNull_WhenUpdatingNonExistentStudent()
    {
        // tegevus
        var result = await Svc<IStudentServices>().UpdateAsync(Guid.NewGuid(), CreateDefaultDTO());

        // kontroll
        Assert.Null(result);
    }

    [Fact]
    public async Task Should_KeepOldName_WhenUpdateNameIsEmpty()
    {
        var dto = CreateDefaultDTO();
        var created = await Svc<IStudentServices>().Create(dto);

        dto.FirstName = "";
        var result = await Svc<IStudentServices>().UpdateAsync(created.Id, dto);

        Assert.Equal("Nikita", result.FirstName);
    }

    [Fact]
    public async Task Should_KeepGrades_WhenUpdateGradesIsNull()
    {
        // ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<IStudentServices>().Create(dto);
        var oldGrades = created.Grades.ToList(); // kopeeri list

        dto.Grades = null;
        var result = await Svc<IStudentServices>().UpdateAsync(created.Id, dto);

        // kontroll
        Assert.NotNull(result);
        Assert.NotNull(result.Grades);
        Assert.Equal(oldGrades, result.Grades);
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
