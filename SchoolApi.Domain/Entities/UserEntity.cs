using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Domain.Entities;

public class UserEntity {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();  
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; } = string.Empty; 
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
    
    [Required]
    public string Role { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    
    public int YearOfBirth { get; set; }
    
  
    [ForeignKey("TablePoint")]
    public Guid? TableId { get; set; }
    [InverseProperty("Student")]
    public PointEntity TablePoint { get; set; }
    
    [ForeignKey("Class")]
    public Guid? ClassId { get; set; }  
    public ClassEntity? Class { get; set; }
    
    public string? AccessToken { get; set; }
    public int? ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}

