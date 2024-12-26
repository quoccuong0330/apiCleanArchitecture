using System.ComponentModel.DataAnnotations;

namespace SchoolApi.API.Dtos.Requests;

public class UpdateUserDto {
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; } = string.Empty; 
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
   
    [Required]
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int YearOfBirth { get; set; }
}