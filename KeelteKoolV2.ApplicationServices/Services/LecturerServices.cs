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

    public async Task<Lecturer> Create(LecturerDTO dto)
    {
        if (dto == null)
            return null;

        var domain = new Lecturer();
        domain.Id = Guid.NewGuid();

        if (dto.FirstName.Length < 1)
        {
            dto.FirstName = "John";
        }
        if (dto.LastName.Length < 1)
        {
            dto.LastName = "Doe";
        }

        domain.FirstName = dto.FirstName;
        domain.LastName = dto.LastName;
        domain.Qualifications = dto.Qualifications;
        domain.UserID = dto.UserID;
        domain.CreatedAt = DateTime.Now;
        domain.ModifiedAt = DateTime.Now;

        await _context.Lecturers.AddAsync(domain);
        await _context.SaveChangesAsync();
        //tagastatakse domeenile kuuluv objekt
        return domain;
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
