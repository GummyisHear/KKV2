using System.ComponentModel.DataAnnotations;

namespace KeelteKoolV2.Models.Students;

public class StudentCreateUpdateViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Eesnimi on kohustuslik")]
    [Display(Name = "Eesnimi")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Perekonnanimi on kohustuslik")]
    [Display(Name = "Perekonnanimi")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Hinded (eraldatud komaga)")]
    public string Grades { get; set; } = string.Empty;
}