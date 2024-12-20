using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Domain.Entities;

public class ClassEntity {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();  
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("Lead")]
    public Guid? LeadId { get; set; }  
    public UserEntity? Lead { get; set; }
    
    public ICollection<UserEntity> Students { get; set; } = new List<UserEntity>();
    
    [NotMapped]
    public int StudentCount => Students?.Where(s => s.Role.ToLower() == "student").Count() ?? 0;
}