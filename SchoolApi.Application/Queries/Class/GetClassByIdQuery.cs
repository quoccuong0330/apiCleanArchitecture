using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Queries;

public record GetClassByIdQuery(Guid id) : IRequest<Result<ClassEntity>>;

public class GetClassByIdQueryHandler(IClassRepository classRepository)
    : IRequestHandler<GetClassByIdQuery, Result<ClassEntity>> {
    public async Task<Result<ClassEntity>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken) {
        return await classRepository.GetClassByIdAsync(request.id);
    }
}