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
    [HttpPost("")]
    public async Task<IActionResult> AddClassAsync([FromBody] CreateClassDto classDto) {
        try {
            var classEntity = classDto.ToEntity();
            var result = await sender.Send(new AddClassCommand(classEntity));
            return Ok(result.ToResponseDto());
        }catch (ValidationException ex) {
             return BadRequest(new { ex.Message });
        }
    }
    
    [HttpGet("get-all-class")]
    public async Task<IActionResult> GetAllClassAsync() {
        var result = await sender.Send(new GetAllClassQuery());
        return Ok(result);
    }
    
    [HttpGet("get-class/{classId}")]
    public async Task<IActionResult> GetAllClassAsync([FromRoute] Guid classId) {
        try {
            var result = await sender.Send(new GetClassByIdQuery(classId));
            return Ok(result.ToResponseDto());
        }
        catch (NotFoundException ex) {
            return NotFound(new { ex.Message });
        }
    }
    
    [HttpPut("update-class/{classId}")]
    public async Task<IActionResult> UpdateClassAsync([FromRoute] Guid classId, [FromBody] UpdateClassDto updateClassDto) {
        try {
            var classEntity = updateClassDto.ToEntity();
            var result = await sender.Send(new UpdateClassCommand(classId, classEntity));
            return  Ok(result.ToResponseDto());
        }
        catch (NotFoundException ex) {
            return NotFound(new { ex.Message });
        }
    }
    
    [HttpDelete("delete-class/{classId}")]
        public async Task<IActionResult> DeleteClassAsync([FromRoute] Guid classId) {
            try {
                var result = await sender.Send(new DeleteClassCommand(classId));
                return Ok(result);
            }
            catch (NotFoundException ex) {
                return NotFound(new { ex.Message });
            }
    }
}