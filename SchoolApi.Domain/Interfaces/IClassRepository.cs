using SchoolApi.Domain.Entities;

namespace SchoolApi.Infrastructure.Interfaces;

public interface IClassRepository {
    public Task<IEnumerable<ClassEntity>> GetAllClassAsync();
    public Task<ClassEntity> GetClassByIdAsync(Guid id);
    public Task<ClassEntity> CreateClassAsync(ClassEntity classEntity);
    public Task<ClassEntity> UpdateClassAsync(Guid id, ClassEntity classEntity);
    public Task<bool> DeleteClassByIdAsync(Guid id);
}