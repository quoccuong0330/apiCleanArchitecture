using MediatR;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record DeleteClassCommand(Guid id) : IRequest<Result<ClassEntity>> { };

public class DeleteClassCommandHandler(IClassRepository classRepository) : IRequestHandler<DeleteClassCommand, Result<ClassEntity>> {
    public async Task<Result<ClassEntity>> Handle(DeleteClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.DeleteClassByIdAsync(request.id);
    }
}