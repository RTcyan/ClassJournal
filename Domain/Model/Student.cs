namespace Domain.Model;

public class Student
{
    public Guid Id { get; set; }
    public Grade Grade { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string ParentsFullName { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public string Sex { get; set; } = string.Empty;
    public int PersonalLifeNumber { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ParentsPhoneNumber { get; set; } = string.Empty;
}

