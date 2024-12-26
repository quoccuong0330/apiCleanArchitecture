using SchoolApi.API.Dtos.Requests;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Application.Extensions;

public static class UserMappingExtensions {
    public static UserEntity ToEntity(this UserDto userDto) {
        return new UserEntity {
            Email = userDto.Email,
            Password = userDto.Password,
            ClassId = userDto.ClassId,
            YearOfBirth = userDto.YearOfBirth,
            Name = userDto.Name,
            Phone = userDto.Phone,
            Role = userDto.Role
        };
    }
    
    public static UserEntity ToEntity(this UpdateUserDto userDto) {
        return new UserEntity {
            Email = userDto.Email,
            Address = userDto.Address,
            YearOfBirth = userDto.YearOfBirth,
            Name = userDto.Name,
            Phone = userDto.Phone,
        };
    }

    public static UserResponseDto toResponseDto(this UserEntity userEntity) {
        return new UserResponseDto {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Role = userEntity.Role,
            Email = userEntity.Email,
            Phone = userEntity.Phone,
            Address = userEntity.Address,
            YearOfBirth = userEntity.YearOfBirth,
            TeacherName = userEntity.Class?.Lead?.Name,
            ClassName = userEntity.Class?.Name,
        };
    }
}