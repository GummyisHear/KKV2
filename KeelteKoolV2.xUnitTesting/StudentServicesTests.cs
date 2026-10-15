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
