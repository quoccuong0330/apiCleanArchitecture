using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Queries.User;

public record GetUserByIdQuery(Guid userId) : IRequest<Result<UserEntity>> {
    
}

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery,Result<UserEntity>> {
    public async Task<Result<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
        return await userRepository.GetUserById(request.userId);
    }
}