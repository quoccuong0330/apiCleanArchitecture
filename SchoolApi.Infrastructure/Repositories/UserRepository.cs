using Microsoft.EntityFrameworkCore;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Application.Extensions;
using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;
using SchoolApi.Domain.InterfaceRepositories;
using SchoolApi.Infrastructure.Data;

namespace SchoolApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository {
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<Result<IEnumerable<UserEntity>>> GetAllUser() {
        var listUser = await _context.User
            .Include(x=>x.Class)
            .Include(x=>x.Class.Lead)
            .ToListAsync();
        return Result<IEnumerable<UserEntity>>.Success(listUser);
    }

    public async Task<Result<UserEntity>> GetUserById(Guid userId) {
        var modelUser = await _context.User
            .Include(x=>x.Class)
            .Include(x=>x.Class.Lead).FirstOrDefaultAsync(x => x.Id.Equals(userId));
        return modelUser is null ? Result<UserEntity>.Failure($"Not found user with id {userId}") :
            Result<UserEntity>.Success(modelUser);
    }
    
    public async Task<Result<UserEntity>> CreateStudent(UserEntity userEntity) {
        var checkClass = await _context.Class.FirstOrDefaultAsync(x => x.Id.Equals(userEntity.ClassId));
        await _context.SaveChangesAsync();
        if (checkClass is null) return Result<UserEntity>.Failure("Class is not exists");
        
        var checkEmail = await _context.User.AnyAsync(x => x.Email.Equals(userEntity.Email));
        if (checkEmail) return Result<UserEntity>.Failure("Email has exists");
        
        var modelUser = await _context.User.AddAsync(userEntity);
        if (modelUser is null) return Result<UserEntity>.Failure("Create new user fail");

        var tableEntity = new PointEntity {
            Id = new Guid(),
            Chemistry = 0,
            Math = 0,
            Physical = 0,
            StudentId = modelUser.Entity.Id,
            EditorId = checkClass?.LeadId
        };
        
        var modelPoint = await _context.Point.AddAsync(tableEntity);
        await _context.SaveChangesAsync();
        if (modelPoint is null) return Result<UserEntity>.Failure("Create point of user fail");
        modelPoint.Entity.StudentId = modelUser.Entity.Id;
        await _context.SaveChangesAsync();

        var model = await _context.User
            .Include(x=>x.Class)
            .Include(x=>x.Class.Lead)
            .FirstOrDefaultAsync(x => x.Id.Equals(modelUser.Entity.Id));

        return Result<UserEntity>.Success(model);
    }

    public async Task<Result<UserEntity>> CreateTeacher(UserEntity userEntity) {
            if (userEntity.ClassId is null) {
                var checkEmail = await _context.User.AnyAsync(x => x.Email.Equals(userEntity.Email));
                if (checkEmail) return Result<UserEntity>.Failure("Email has exists");
                    
                var modelUser = await _context.User.AddAsync(userEntity);
                await _context.SaveChangesAsync();

                return Result<UserEntity>.Success(modelUser.Entity);
            }
            else {
                var checkClass = await _context.Class
                    .Include(x=>x.Lead)
                    .FirstOrDefaultAsync(x => x.Id.Equals(userEntity.ClassId));
                if (checkClass is null && userEntity.ClassId.HasValue) {
                    return Result<UserEntity>.Failure("Class does not exists");
                }
                if (checkClass?.Lead?.Id is not null) {
                    return Result<UserEntity>.Failure("Class already has teacher");
                }
                var modelUser = await _context.User.AddAsync(userEntity);
                await _context.SaveChangesAsync();
                checkClass.LeadId = modelUser.Entity.Id;
                await _context.SaveChangesAsync();
                return Result<UserEntity>.Success(modelUser.Entity);
            }
    }

    public async Task<Result<UserEntity>> CreateAdmin(UserEntity userEntity) {
        var checkEmail = await _context.User.AnyAsync(x => x.Email.Equals(userEntity.Email));
        if (checkEmail) return Result<UserEntity>.Failure("Email has exists");
        
        var modelUser = await _context.User.AddAsync(userEntity);
        await _context.SaveChangesAsync();
       
        return Result<UserEntity>.Success(modelUser.Entity);
    }

    public async Task<Result<UserEntity>> UpdateUser(Guid userId, UserEntity userEntity) {
        
        var modelUser = await _context.User
            .Include(x=>x.Class)
            .Include(x=>x.Class.Lead)
            .FirstOrDefaultAsync(x => x.Id.Equals(userId));
        
        var checkEmail = await _context.User.AnyAsync(x => x.Email.Equals(userEntity.Email) && x.Id!=modelUser.Id);
        if (checkEmail) return Result<UserEntity>.Failure("Email has exists");
        
        if (modelUser is null) return Result<UserEntity>.Failure("Not found");

        modelUser.YearOfBirth = userEntity.YearOfBirth;
        modelUser.Name = userEntity.Name;
        modelUser.Address = userEntity.Address;
        modelUser.Email = userEntity.Email;
        modelUser.Phone = userEntity.Phone;

        await _context.SaveChangesAsync();
        return Result<UserEntity>.Success(modelUser);
    }

    public async Task<Result<UserEntity>> ChangeClassUser(Guid userId, Guid idNewClass) {
        var classModel = await _context.Class.FirstOrDefaultAsync(c=>c.Id.Equals(idNewClass));
        if (classModel is null) return Result<UserEntity>.Failure($"Not found class with {idNewClass}");

        var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        if (user is null) return Result<UserEntity>.Failure($"Not found user with {idNewClass}");

        if (user.Role.Equals("student")) {
            user.ClassId = idNewClass;
            var point = await _context.Point.FirstOrDefaultAsync(x => x.StudentId.Equals(userId));
            point.EditorId = classModel.LeadId;

        } else if (user.Role.Equals("teacher")) {
            if (classModel.LeadId.HasValue) {
                var oldTeacher = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(classModel.LeadId));
                oldTeacher!.ClassId = null;
                user.ClassId = idNewClass;
                
                var points = await _context.Point.ToListAsync();

                foreach (var point in points.Where(point => point.EditorId.Equals(oldTeacher.Id))) {
                    point.EditorId = userId;
                }
                await _context.SaveChangesAsync();
            }
            classModel.LeadId = user.Id;
        }
        else {
            return Result<UserEntity>.Failure("Bad request");
        }
        await _context.SaveChangesAsync();
        return Result<UserEntity>.Success(user);
    }
    
   


    public async Task<Result<UserEntity>> DeleteUserById(Guid userId) {
        var modelUser = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(userId));
        if (modelUser is null) return Result<UserEntity>.Failure("Not found");

        var table = await _context.Point.FirstOrDefaultAsync(x => x.StudentId.Equals(modelUser.Id));
        if (table is not null) _context.Point.Remove(table);
         _context.User.Remove(modelUser);
         await _context.SaveChangesAsync();

         return Result<UserEntity>.Success(modelUser);
    }
    
}