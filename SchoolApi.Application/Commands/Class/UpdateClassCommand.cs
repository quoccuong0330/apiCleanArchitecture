using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record UpdateClassCommand(Guid ClassId,ClassEntity classEntity) : IRequest<Result<ClassEntity>> ;

public class UpdateClassCommandHandler(IClassRepository classRepository)
    : IRequestHandler<UpdateClassCommand, Result<ClassEntity>> {
    public async Task<Result<ClassEntity>> Handle(UpdateClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.UpdateClassAsync(request.ClassId,request.classEntity);
    }
}