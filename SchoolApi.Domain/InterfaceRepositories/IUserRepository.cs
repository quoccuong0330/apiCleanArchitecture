using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Domain.InterfaceRepositories;

public interface IUserRepository {
    public Task<Result<IEnumerable<UserEntity>>> GetAllUser();
    public Task<Result<UserEntity>>  GetUserById(Guid userId);
    public Task<Result<UserEntity>>  CreateStudent(UserEntity userEntity);
    public Task<Result<UserEntity>>  CreateTeacher(UserEntity userEntity);
    public Task<Result<UserEntity>>  CreateAdmin(UserEntity userEntity);
    public Task<Result<UserEntity>> UpdateUser(Guid userId,UserEntity userEntity);
    public  Task<Result<UserEntity>> ChangeClassUser(Guid studentId, Guid idNewClass);
    public Task<Result<UserEntity>> DeleteUserById(Guid userId);
}