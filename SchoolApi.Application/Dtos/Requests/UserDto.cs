using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Dtos.Requests;

public class UserDto {
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; } = string.Empty; 
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int YearOfBirth { get; set; }
    public Guid? ClassId { get; set; }  
    public string Role { get; set; } = "student";
}

public class StudentDto : UserDto {
    public Guid? ClassId { get; set; }  
    public string Role { get; set; } = "student";
}