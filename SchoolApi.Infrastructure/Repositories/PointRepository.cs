using Microsoft.EntityFrameworkCore;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Repositories;

public class PointRepository(ApplicationDbContext _context) : IPointRepository {
    
    public async Task<Result<PointEntity>> GetPointById(Guid studentId) {
        var model = await _context.Point
            .Include(x=>x.Student)
            .Include(x=>x.Editor.Class)
            .FirstOrDefaultAsync(x => x.StudentId.Equals(studentId));
        return model is null ? Result<PointEntity>.Failure("Not found") : Result<PointEntity>.Success(model);
    }

    public async Task<Result<PointEntity>> UpdatePoint(Guid studentId, PointEntity pointEntity) {
        var model = await _context.Point
            .Include(x=>x.Student)
            .Include(x=>x.Editor.Class)
            .FirstOrDefaultAsync(x => x.StudentId.Equals(studentId));
        if (model is null) return Result<PointEntity>.Failure("Not found");

        model.Chemistry = pointEntity.Chemistry;
        model.Math = pointEntity.Math;
        model.Physical = pointEntity.Physical;
        await _context.SaveChangesAsync();
        
        return Result<PointEntity>.Success(model);
    }
}