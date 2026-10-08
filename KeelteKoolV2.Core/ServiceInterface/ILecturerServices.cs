using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;

namespace KeelteKoolV2.Core.ServiceInterface;

public interface ILecturerServices
{
    public Task<Lecturer> Create(LecturerDTO dto);
    public Task<Lecturer> Update(LecturerDTO dto);
    public Task<Lecturer> DetailAsync(Guid id);
    public Task<Lecturer> Delete(Guid id);

}
