using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;

namespace SchoolApi.Application.Commands.Point;

public record UpdatePointCommand(Guid id,PointEntity pointEntity) : IRequest<Result<PointEntity>> {
    
}

public class UpdatePointCommandHandler(IPointRepository pointRepository) : IRequestHandler<UpdatePointCommand, Result<PointEntity>> {
    public async Task<Result<PointEntity>> Handle(UpdatePointCommand request, CancellationToken cancellationToken) {
        return await pointRepository.UpdatePoint(request.id,request.pointEntity);
    }
}