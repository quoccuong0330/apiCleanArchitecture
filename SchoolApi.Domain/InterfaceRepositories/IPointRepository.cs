using SchoolApi.Application.ResponseObject;
using SchoolApi.Domain.Entities;

namespace SchoolApi.Domain.InterfaceRepositories;

public interface IPointRepository {
    public Task<Result<PointEntity>> GetPointById(Guid studentId);
    public Task<Result<PointEntity>> UpdatePoint(Guid studentId,PointEntity pointEntity);
}