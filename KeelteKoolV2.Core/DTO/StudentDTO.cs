using Microsoft.AspNetCore.Http;

namespace KeelteKoolV2.Core.DTO;

public class StudentDTO
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserID { get; set; }
    public List<IFormFile>? Files { get; set; }
    public IEnumerable<FileToDatabaseDTO>? Image { get; set; } = new List<FileToDatabaseDTO>();
    public List<LanguageCourseDTO>? LanguageCourses { get; set; } = [];
    public List<string>? Grades { get; set; } = [];
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
