using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Infrastructure.Interfaces;

public interface IClassRepository {
    public Task<IEnumerable<ClassEntity>> GetAllClassAsync();
    public Task<Result<ClassEntity>> GetClassByIdAsync(Guid id);
    public Task<Result<ClassEntity>> CreateClassAsync(ClassEntity classEntity);
    public Task<Result<ClassEntity>> UpdateClassAsync(Guid id, ClassEntity classEntity);
    public Task<Result<ClassEntity>> DeleteClassByIdAsync(Guid id);
}