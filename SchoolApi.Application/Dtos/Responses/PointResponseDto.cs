namespace SchoolApi.API.Dtos.Responses;

public class PointResponseDto {
    public string StudentName { get; set; }
    public double Math { get; set; }
    public double Physical { get; set; }
    public double Chemistry { get; set; }
    public double Average { get; set; }
    public string ClassName { get; set; }
    public string TeacherName { get; set; }
}