using KeelteKoolV2.Core.Domain;

namespace KeelteKoolV2.Core.ServiceInterface;

public interface IStudentServices
{
    public Task<Student> Create(Lecturer dto);
    public Task<Student> Update(Lecturer dto);
    public Task<Student> DetailAsync(Guid id);
    public Task<Student> Delete(Guid id);
}
