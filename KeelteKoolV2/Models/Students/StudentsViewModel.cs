using KeelteKoolV2.Core.DTO;

namespace KeelteKoolV2.Models.Students;

public class StudentsViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<IFormFile>? Files { get; set; }
    public IEnumerable<FileToDatabaseDTO>? Image { get; set; } = new List<FileToDatabaseDTO>();
    public List<LanguageCourseDTO>? LanguageCourses { get; set; } = [];
    public DateTime? CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public string FullName => $"{FirstName ?? ""} {LastName ?? ""}";
}
