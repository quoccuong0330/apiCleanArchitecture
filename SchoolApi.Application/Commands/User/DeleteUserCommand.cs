using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Commands.User;

public record DeleteUserCommand(Guid id) : IRequest<Result<UserEntity>> {
    
}

public class DeleteUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<DeleteUserCommand, Result<UserEntity>> {
    public async Task<Result<UserEntity>> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        return await userRepository.DeleteUserById(request.id);
    }
}