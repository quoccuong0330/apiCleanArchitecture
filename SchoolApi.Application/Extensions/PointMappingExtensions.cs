using SchoolApi.API.Dtos.Requests;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Application.Extensions;

public static class PointMappingExtensions {
    public static PointEntity ToEntity(this UpdatePointDto updatePointDto) {
        return new PointEntity {
            Math = updatePointDto.Math,
            Physical = updatePointDto.Physical,
            Chemistry = updatePointDto.Chemistry
        };
    }

    public static PointResponseDto ToDto(this PointEntity pointEntity) {
        return new PointResponseDto {
            StudentName = pointEntity.Student.Name,
            Math = pointEntity.Math,
            Chemistry = pointEntity.Chemistry,
            Physical = pointEntity.Physical,
            Average = pointEntity.Average,
            TeacherName = pointEntity.Editor?.Name,
            ClassName = pointEntity.Editor?.Class?.Name
        };
    }
}