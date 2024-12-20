using SchoolApi.API.Dtos.Requests;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Application.Extensions;

public static class ClassMappingExtensions {
    public static ClassEntity ToEntity(this CreateClassDto dto)
    {
        return new ClassEntity
        {
            Name = dto.ClassName
        };
    }
    
    public static ClassEntity ToEntity(this UpdateClassDto dto)
    {
        return new ClassEntity
        {
            Name = dto.ClassName
        };
    }

    public static ClassResponseDto ToResponseDto(this ClassEntity entity)
    {
        return new ClassResponseDto
        {
            Id = entity.Id,
            ClassName = entity.Name,
            TeacherName = entity.Lead?.Name,
            Students = entity.Students,
            StudentCount = entity.StudentCount,
        };
    }
}