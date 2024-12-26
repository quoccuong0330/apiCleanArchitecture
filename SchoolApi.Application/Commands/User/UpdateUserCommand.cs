using MediatR;
using SchoolApi.API.Dtos.Requests;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Commands.User;

public record UpdateUserCommand(Guid id,UserEntity updateUserDto) : IRequest<Result<UserEntity>> {
    
}

public class UpdateUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand, Result<UserEntity>> {
    public async Task<Result<UserEntity>> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
        return await userRepository.UpdateUser(request.id ,request.updateUserDto);
    }
}