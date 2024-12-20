using MediatR;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record class AddClassCommand(ClassEntity classModel) : IRequest<ClassEntity>;

public class AddClassCommandHandler(IClassRepository classRepository) 
    : IRequestHandler<AddClassCommand,ClassEntity> {
    public async Task<ClassEntity> Handle(AddClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.CreateClassAsync(request.classModel);
    }
}