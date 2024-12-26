using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.ResponseObject;
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
        var list = await _context.Class
            .Include(x=>x.Students)
            .Include(x=>x.Lead)
            .ToListAsync();
        return list;
    }

    public async Task<Result<ClassEntity>> GetClassByIdAsync(Guid id) {
        var modelClass = await _context.Class
            .Include(x=>x.Students)
            .Include(x=>x.Lead)
            .FirstOrDefaultAsync(c=>c.Id == id);
        if (modelClass is null) return Result<ClassEntity>.Failure("Not found");
        return Result<ClassEntity>.Success(modelClass);
    }

    public async Task<Result<ClassEntity>> CreateClassAsync(ClassEntity classEntity) {
        var checkName = await _context.Class.AnyAsync(c=>c.Name.ToLower().Equals(classEntity.Name.ToLower()));
        if (checkName) return Result<ClassEntity>.Failure("Name has already exists");
         var modelClass = await _context.AddAsync(classEntity);
         await _context.SaveChangesAsync();
         var model = await _context.Class.FirstOrDefaultAsync(x => x.Id.Equals(classEntity.Id));

         return model is not null ? Result<ClassEntity>.Success(model) :              
             Result<ClassEntity>.Failure("Failed to create class or retrieve it.");
    }

    public async Task<Result<ClassEntity>> UpdateClassAsync(Guid id, ClassEntity? classEntity) {
        var checkName = await _context.Class.AnyAsync(c=>c.Name.ToLower().Equals(classEntity.Name.ToLower()));
        if (checkName) return Result<ClassEntity>.Failure("Name has already exists");
       var classModel = await _context.Class
           .Include(x=>x.Lead)
           .Include(x=>x.Students)
           .FirstOrDefaultAsync(c=>c.Id == id);
       if (classModel is null) return Result<ClassEntity>.Failure($"Not found class with {id}");
       
       classModel.Name = classEntity!.Name;
       await _context.SaveChangesAsync();
       return Result<ClassEntity>.Success(classModel);
    }
   

    public async Task<Result<ClassEntity>> DeleteClassByIdAsync(Guid id) {
        var classModel = await _context.Class.FirstOrDefaultAsync(c=>c.Id == id);
        if (classModel is null) return Result<ClassEntity>.Failure($"Not found class with {id}");;;
        
        _context.Remove(classModel);
        await _context.SaveChangesAsync();
        return Result<ClassEntity>.Success(classModel);
    }
}