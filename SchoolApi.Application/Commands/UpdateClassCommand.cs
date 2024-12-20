using MediatR;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record UpdateClassCommand(Guid ClassId,ClassEntity? classEntity) : IRequest<ClassEntity> ;

public class UpdateClassCommandHandler(IClassRepository classRepository)
    : IRequestHandler<UpdateClassCommand, ClassEntity> {
    public async Task<ClassEntity?> Handle(UpdateClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.UpdateClassAsync(request.ClassId,request.classEntity);
    }
}