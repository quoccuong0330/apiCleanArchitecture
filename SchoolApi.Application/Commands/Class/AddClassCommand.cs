using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record class AddClassCommand(ClassEntity classModel) : IRequest<Result<ClassEntity>>;

public class AddClassCommandHandler(IClassRepository classRepository) 
    : IRequestHandler<AddClassCommand,Result<ClassEntity>> {
    public async Task<Result<ClassEntity>> Handle(AddClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.CreateClassAsync(request.classModel);
    }
}