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
        var dto = new LecturerDTO();
        dto.FirstName = "Test";
        dto.LastName = "Test";
        dto.Qualifications = "Testicles";

        // tegevus
        var result = await Svc<ILecturerServices>().Create(dto);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_ShowDetails_WhenResultIsReturned()
    {
        //ülesseade
        var dto = new LecturerDTO();
        dto.FirstName = "Test";
        dto.LastName = "Test";
        dto.Qualifications = "Testicles";

        // tegevus
        var result = await Svc<ILecturerServices>().DetailAsync(dto.Id);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_UpdateObject_WhenResultIsReturned()
    {
        //ülesseade
        var dto = new LecturerDTO();
        dto.FirstName = "Test";
        dto.LastName = "Test";
        dto.Qualifications = "Testicles";

        // tegevus
        var result = await Svc<ILecturerServices>().Update(dto);

        //kontroll
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Should_DeleteObject_WhenResultIsReturned()
    {
        //ülesseade
        var dto = new LecturerDTO();
        dto.FirstName = "Test";
        dto.LastName = "Test";
        dto.Qualifications = "Testicles";

        // tegevus
        var result = await Svc<ILecturerServices>().Delete(dto.Id);

        //kontroll
        Assert.NotNull(result);
    }
}
