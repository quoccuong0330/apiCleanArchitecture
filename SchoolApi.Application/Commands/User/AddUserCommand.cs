using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Commands.User;

public record AddUserCommand(UserEntity UserEntity) :  IRequest<Result<UserEntity>> {
    
}

public class AddStudentCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddUserCommand, Result<UserEntity>> {
    public async Task<Result<UserEntity>> Handle(AddUserCommand request, CancellationToken cancellationToken) {
        return request.UserEntity.Role switch {
            "student" => await userRepository.CreateStudent(request.UserEntity),
            "teacher" => await userRepository.CreateTeacher(request.UserEntity),
            "admin" => await userRepository.CreateAdmin(request.UserEntity),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

 