namespace SchoolApi.API.Dtos.Responses;

public class UserResponseDto {
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty; 
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = "student";
    public string Address { get; set; } = string.Empty;
    public int YearOfBirth { get; set; }
    public string? TeacherName { get; set; }
    public string ClassName { get; set; }
}