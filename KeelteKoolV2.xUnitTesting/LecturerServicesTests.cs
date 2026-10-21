using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using Xunit;

namespace KeelteKoolV2.xUnitTesting;

public class LecturerServicesTests : TestBase
{
    /// <summary>
    /// Kontrollib, et Lecturer luuakse korrektselt ja kõik vajalikud väljad salvestatakse õigete väärtustega.
    /// </summary>
    [Fact]
    public async Task Should_CreateObject_WhenResultIsReturned() //create test
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var result = await Svc<ILecturerServices>().Create(dto);

        //kontroll
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(dto.FirstName, result.FirstName);
        Assert.Equal(dto.LastName, result.LastName);
        Assert.Equal(dto.Qualifications, result.Qualifications);
    }


    /// <summary>
    /// Kontrollib, et Lecturerri andmed leitakse õige ID järgi ja tagastatud andmed on õiged.
    /// </summary>
    [Fact]
    public async Task Should_GetLecturerDetails_WhenIdIsNotNull() //detail test
    {
        //ülesseade
        var dto = CreateDefaultDTO();

        // tegevus
        var created = await Svc<ILecturerServices>().Create(dto);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);

        //kontroll
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal(created.FirstName, result.FirstName);
        Assert.Equal(created.LastName, result.LastName);
        Assert.Equal(created.Qualifications, result.Qualifications);
    }


    /// <summary>
    /// Kontrollib, et Lecturerri andmeid saab uuendada ning muudatused salvestatakse õigesti.
    /// </summary>
    [Fact]
    public async Task Should_UpdateFirstNameWithNewData_WhenDataIsDifferentFromDB() //update test
    {
        //ülesseade
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);
        var originalName = created.FirstName;

        var newDto = new LecturerDTO
        {
            Id = created.Id,
            FirstName = "Uusnimi",
            LastName = created.LastName,
            Qualifications = created.Qualifications,
            UserID = created.UserID
        };

        // tegevus
        var result = await Svc<ILecturerServices>().Update(newDto);

        //kontroll
        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
        Assert.Equal("Uusnimi", result.FirstName);
        Assert.NotEqual(originalName, result.FirstName);
        Assert.Equal(created.LastName, result.LastName);
        Assert.Equal(created.Qualifications, result.Qualifications);
    }


    /// <summary>
    /// Kontrollib, et olemasoleva Lecturerri saab kustutada ning pärast kustutamist teda enam andmebaasist ei leita.
    /// </summary>
    [Fact]
    public async Task Should_DeleteLecturer_WhenValidIdIsGiven() //delete test
    {
        //ülesseade
        var dto = CreateDefaultDTO();
        var created = await Svc<ILecturerServices>().Create(dto);

        // tegevus
        var deletedLecturer = await Svc<ILecturerServices>().Delete(created.Id);
        var result = await Svc<ILecturerServices>().DetailAsync(created.Id);

        //kontroll
        Assert.NotNull(deletedLecturer);
        Assert.Equal(created.Id, deletedLecturer.Id);
        Assert.Equal(created.FirstName, deletedLecturer.FirstName);
        Assert.Equal(created.LastName, deletedLecturer.LastName);
        Assert.Null(result);
    }

    private LecturerDTO CreateDefaultDTO()
    {
        return new LecturerDTO
        {
            FirstName = "Test",
            LastName = "Test",
            Qualifications = "Test"
        };
    }
}
