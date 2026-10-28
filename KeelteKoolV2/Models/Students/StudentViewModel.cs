using System.ComponentModel.DataAnnotations;

namespace KeelteKoolV2.Models.Students;

public class StudentViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Eesnimi")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Perekonnanimi")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Hinded")]
    public string Grades { get; set; } = string.Empty;

    [Display(Name = "Loodud")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Muudetud")]
    public DateTime ModifiedAt { get; set; }
}