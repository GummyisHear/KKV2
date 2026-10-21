namespace KeelteKoolV2.Models.Lecturers;

public class LecturerViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Qualifications { get; set; }
    public Guid? UserID { get; set; }
}