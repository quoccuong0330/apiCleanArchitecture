using MediatR;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Queries;

public record GetClassByIdQuery(Guid id) : IRequest<ClassEntity>;

public class GetClassByIdQueryHandler(IClassRepository classRepository)
    : IRequestHandler<GetClassByIdQuery, ClassEntity> {
    public async Task<ClassEntity> Handle(GetClassByIdQuery request, CancellationToken cancellationToken) {
        return await classRepository.GetClassByIdAsync(request.id);
    }
}