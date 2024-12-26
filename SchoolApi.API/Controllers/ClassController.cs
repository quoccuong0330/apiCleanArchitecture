using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.API.Dtos.Requests;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Application.Commands;
using SchoolApi.Application.Exceptions;
using SchoolApi.Application.Extensions;
using SchoolApi.Application.Queries;
using SchoolApi.Domain.Entities;

namespace SchoolApi.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ClassController(ISender sender) : ControllerBase{
    
    [HttpPost("create-class")]
    public async Task<IActionResult> AddClassAsync([FromBody] CreateClassDto classDto) {
        var classEntity = classDto.ToEntity();
        var result = await sender.Send(new AddClassCommand(classEntity));
        return result.IsSuccess ? Ok(result.Data.ToResponseDto()) : BadRequest(result.Message);
    }
    
    [HttpGet("get-all-class")]
    public async Task<IActionResult> GetAllClassAsync() {
        var result = await sender.Send(new GetAllClassQuery());
        List<ClassResponseDto> covertDto = [];
        covertDto.AddRange(result.Select(classEntity => classEntity.ToResponseDto()));
        return Ok(covertDto);
    }
    
    [HttpGet("get-class/{classId}")]
    public async Task<IActionResult> GetAllClassAsync([FromRoute] Guid classId) {
        var result = await sender.Send(new GetClassByIdQuery(classId));
        return result.IsSuccess ? Ok(result.Data.ToResponseDto()) : NotFound();
    }
    
    [HttpPut("update-class/{classId}")]
    public async Task<IActionResult> UpdateClassAsync([FromRoute] Guid classId, [FromBody] UpdateClassDto updateClassDto) {
        var classEntity = updateClassDto.ToEntity();
        var result = await sender.Send(new UpdateClassCommand(classId, classEntity));
        return result.IsSuccess ? Ok(result.Data.ToResponseDto()) : NotFound();
    }
    
    [HttpDelete("delete-class/{classId}")]
    public async Task<IActionResult> DeleteClassAsync([FromRoute] Guid classId) {
        var result = await sender.Send(new DeleteClassCommand(classId));
        return result.IsSuccess ? Ok(result.IsSuccess) : BadRequest(result.IsSuccess);
    }
}