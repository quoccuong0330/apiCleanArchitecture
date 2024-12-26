using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Queries.Point;

public record GetPointByIdQuery(Guid Id) : IRequest<Result<PointEntity>> {
    
}

public class GetPointByIdQueryHandle(IPointRepository pointRepository) : IRequestHandler<GetPointByIdQuery,Result<PointEntity>> {
    public async Task<Result<PointEntity>> Handle(GetPointByIdQuery request, CancellationToken cancellationToken) {
        return await pointRepository.GetPointById(request.Id);
    }
}