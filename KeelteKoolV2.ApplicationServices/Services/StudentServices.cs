using KeelteKoolV2.Core.Domain;
using KeelteKoolV2.Core.DTO;
using KeelteKoolV2.Core.ServiceInterface;
using KeelteKoolV2.Data;
using Microsoft.EntityFrameworkCore;

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
            Grades = dto.Grades ?? [],
            UserID = dto.UserID,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now
        };

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        return student;
    }

    public async Task<Student> DetailAsync(Guid id)
    {
        var result = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<Student> DeleteAsync(Guid id)
    {
        var entity = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        _context.Students.Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Student> UpdateAsync(Guid id, StudentDTO dto)
    {
        var entity = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        if (!string.IsNullOrEmpty(dto.FirstName))
            entity.FirstName = dto.FirstName;
        if (!string.IsNullOrEmpty(dto.LastName))
            entity.LastName = dto.LastName;
        if (dto.LanguageCourses != null)
            entity.LanguageCourses = dto.LanguageCourses;
        if (dto.Grades != null)
            entity.Grades = dto.Grades;

        await _context.SaveChangesAsync();
        return entity;
    }
}
