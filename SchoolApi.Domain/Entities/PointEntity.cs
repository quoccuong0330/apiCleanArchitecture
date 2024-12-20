using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApi.Domain.Entities;

public class PointEntity {
    [Key] public Guid Id { get; set; } = Guid.NewGuid(); 
    public double Math { get; set; }
    public double Chemistry { get; set; }
    public double Physical { get; set; }
    
    [NotMapped]
    public double Average => (Math + Chemistry + Physical) / 3;  

    [ForeignKey("User")]
    public Guid StudentId { get; set; }
    [InverseProperty("TablePoint")]
    public UserEntity Student { get; set; }

    [ForeignKey("Editor")]
    public Guid? EditorId { get; set; }  
    public UserEntity? Editor { get; set; }
}

