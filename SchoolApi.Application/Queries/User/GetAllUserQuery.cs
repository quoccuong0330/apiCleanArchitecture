using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Queries.User;

public record GetAllUserQuery : IRequest<Result<IEnumerable<UserEntity>>>;

public class GetAllUserQueryHandle(IUserRepository userRepository) : IRequestHandler<GetAllUserQuery,Result<IEnumerable<UserEntity>>> {
    public async Task<Result<IEnumerable<UserEntity>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken) {
        return await userRepository.GetAllUser();
    }
}