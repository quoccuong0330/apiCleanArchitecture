using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Commands.User;

public record UpdateClassOfUserCommand(Guid userId, Guid classId) : IRequest<Result<UserEntity>> {
    
}

public class UpdateClassOfUserCommandHandler(IUserRepository userRepository) :
IRequestHandler<UpdateClassOfUserCommand,Result<UserEntity>> {
    public async Task<Result<UserEntity>> Handle(UpdateClassOfUserCommand request, CancellationToken cancellationToken) {
        return await userRepository.ChangeClassUser(request.userId, request.classId);
    }
}