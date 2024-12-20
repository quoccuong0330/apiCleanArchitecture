using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Dtos.Responses;

public class ClassResponseDto {
    public Guid Id { get; set; }  
    public string ClassName { get; set; }
    public string TeacherName { get; set; }
    public ICollection<UserEntity> Students { get; set; }
    public int StudentCount { get; set; }

    // public int StudentCount => Students?.Where(s => s.Role.ToLower() == "student").Count() ?? 0;
}