using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.Exceptions;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Data;
using SchoolApi.Infrastructure.Interfaces;

namespace SchoolApi.Infrastructure.Repositories;

public class ClassRepository: IClassRepository {
    private readonly ApplicationDbContext _context;

    public ClassRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<IEnumerable<ClassEntity>> GetAllClassAsync() {
        return await _context.Class.ToListAsync();
    }

    public async Task<ClassEntity> GetClassByIdAsync(Guid id) {
        var modelClass = await _context.Class.FirstOrDefaultAsync(c=>c.Id == id);
        if (modelClass is null) throw new NotFoundException("Class",id);
        return modelClass;
    }

    public async Task<ClassEntity> CreateClassAsync(ClassEntity classEntity) {
         classEntity.Id = Guid.NewGuid();
         var classModel =_context.AddAsync(classEntity);
         await _context.SaveChangesAsync();
         return classEntity;
    }

    public async Task<ClassEntity> UpdateClassAsync(Guid id, ClassEntity? classEntity) {
       var classModel = await _context.Class.FirstOrDefaultAsync(c=>c.Id == id);
       if (classModel is null)  throw new NotFoundException("Update Class",id);;
       classModel.Name = classEntity!.Name;
       await _context.SaveChangesAsync();
       return classEntity;
    }

    public async Task<bool> DeleteClassByIdAsync(Guid id) {
        var classModel = await _context.Class.FirstOrDefaultAsync(c=>c.Id == id);
        if (classModel is null)  throw new NotFoundException("Delete Class",id);

        _context.Remove(classModel);
        await _context.SaveChangesAsync();
        return true;
    }
}