using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;

namespace KeelteKoolV2.ApplicationServices.Services;

public class StudentServices : IStudentServices
{
    private readonly KeelteKoolV2Context _context;

    public StudentServices(KeelteKoolV2Context context)
    {
        _context = context;
    }

    public async Task<Student> Create(StudentDTO dto)
    {
        if (dto == null)
            return null;

        if (dto.FirstName.Length < 1)
            dto.FirstName = "John";
        if (dto.LastName.Length < 1)
            dto.LastName = "Doe";

        var student = new Student
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            LanguageCourses = dto.LanguageCourses ?? [],
            UserID = dto.UserID,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now
        };

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public Task<Student> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> DetailAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Student> Update(StudentDTO dto)
    {
        throw new NotImplementedException();
    }
}
