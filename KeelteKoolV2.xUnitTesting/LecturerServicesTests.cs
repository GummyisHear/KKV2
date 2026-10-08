using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using Xunit;

namespace KeelteKoolV2.xUnitTesting;

public class LecturerServicesTests : TestBase
{
    [Fact]
    public async Task Should_CreateObject_WhenResultIsReturned()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var result = await Svc<ILecturerServices>().Create(dto);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_GetLecturerDetails_WhenIdIsNotNull()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        await Svc<ILecturerServices>().Create(dto);
        var result = await Svc<ILecturerServices>().DetailAsync(dto.Id);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_UpdateFirstNameWithNewData_WhenDataIsDifferentFromDB()
    {
        //ülesseade
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var originalName = created.FirstName;

        var newDto = new LecturerDTO();
        dto.Id = created.Id;
        dto.FirstName = "Uusnimi";

        // tegevus
        var result = await Svc<ILecturerServices>().Update(newDto);

        //kontroll
        Assert.NotNull(result);
        Assert.NotEqual(originalName, result.FirstName);
    }

    [Fact]
    public async Task Should_DeleteLecturer_WhenValidIdIsGiven()
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        await Svc<ILecturerServices>().Delete(dto.Id);
        var result = Svc<ILecturerServices>().DetailAsync(dto.Id);

        //kontroll
        Assert.Null(result);
    }

    private LecturerDTO CreateDefaultDTO()
    {
        return new LecturerDTO
        {
            FirstName = "Test",
            LastName = "Test",
            Qualifications = "Testicles"
        };
    }
}
