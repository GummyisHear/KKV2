using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;

namespace KeelteKoolV2.Core.ServiceInterface;

public interface IStudentServices
{
    public Task<Student> Create(StudentDTO dto);
    public Task<Student> Update(StudentDTO dto);
    public Task<Student> DetailAsync(Guid id);
    public Task<Student> Delete(Guid id);
}
