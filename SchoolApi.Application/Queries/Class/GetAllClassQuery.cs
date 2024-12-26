using MediatR;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Queries;

public record GetAllClassQuery : IRequest<IEnumerable<ClassEntity>>;

public class GetAllClassQueryHandler(IClassRepository classRepository)
    : IRequestHandler<GetAllClassQuery, IEnumerable<ClassEntity>> {
    public async Task<IEnumerable<ClassEntity>> Handle(GetAllClassQuery request, CancellationToken cancellationToken) {
        return await classRepository.GetAllClassAsync();
    }
}