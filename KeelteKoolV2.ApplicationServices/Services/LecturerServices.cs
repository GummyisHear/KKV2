using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;

namespace KeelteKoolV2.ApplicationServices.Services;

public class LecturerServices : ILecturerServices
{
    private readonly KeelteKoolV2Context _context;

    public LecturerServices(KeelteKoolV2Context context)
    {
        _context = context;
    }

    public Task<Lecturer> Create(LecturerDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<Lecturer> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Lecturer> DetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Lecturer> Update(LecturerDTO dto)
    {
        throw new NotImplementedException();
    }
}
