using MediatR;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Application.Commands;

public record DeleteClassCommand(Guid id) : IRequest<bool> { };

public class DeleteClassCommandHandler(IClassRepository classRepository) : IRequestHandler<DeleteClassCommand, bool> {
    public async Task<bool> Handle(DeleteClassCommand request, CancellationToken cancellationToken) {
        return await classRepository.DeleteClassByIdAsync(request.id);
    }
}