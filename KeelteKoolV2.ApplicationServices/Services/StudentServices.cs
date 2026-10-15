using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.ServiceInterface;

namespace KeelteKoolV2.ApplicationServices.Services;

public class StudentServices : IStudentServices
{
    public Task<Student> Create(Lecturer dto)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> DetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Update(Lecturer dto)
    {
        throw new NotImplementedException();
    }
}
