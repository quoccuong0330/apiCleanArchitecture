using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.API.Dtos.Requests;
using SchoolApi.API.Dtos.Responses;
using SchoolApi.Application.Commands.User;
using SchoolApi.Application.Extensions;
using SchoolApi.Application.Queries.User;
using SchoolApi.Application.Services;

namespace SchoolApi.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class UserController( ISender sender, IPasswordHasher passwordHasher) : ControllerBase {
    
    [HttpGet("get-all-user")]
    public async Task<IActionResult> GetAllUser() {
        var result = await sender.Send(new GetAllUserQuery());
        IEnumerable<UserResponseDto> list = new List<UserResponseDto>(result.Data.Select(x => x.toResponseDto()));
        return result.IsSuccess ?  Ok(list) : NotFound();
    }
    
    [HttpGet("get-user/{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id) {
        var result = await sender.Send(new GetUserByIdQuery(id));
        return result.IsSuccess ? Ok(result.Data.toResponseDto()) : NotFound(result.Message);
    }

    [HttpPost("create-new-user")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto userDto) {
        userDto.Password = passwordHasher.HashPassword(userDto.Password);
        var userEntity = userDto.ToEntity();
        var result = await sender.Send(new AddUserCommand(userEntity));
        return result.IsSuccess ? Ok(result.Data.toResponseDto()) : NotFound(result.Message);
    }
    
    [HttpPut("update-user/{id:guid}")]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto) {
        var result = await sender.Send(new UpdateUserCommand(id,updateUserDto.ToEntity()));
        return result.IsSuccess ? Ok(result.Data.toResponseDto()) : NotFound(result.Message);
    }
    
    [HttpPut("update-class-user/{userId:guid}")]
    public async Task<IActionResult> UpdateClassUser([FromRoute] Guid userId, [FromBody] Guid classId) {
        var result = await sender.Send(new UpdateClassOfUserCommand(userId,classId));
        return result.IsSuccess ? Ok(result.Data.toResponseDto()) : NotFound(result.Message);
    }
    
    [HttpDelete("delete-user/{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id) {
        var result = await sender.Send(new DeleteUserCommand(id));
        return result.IsSuccess ? Ok(result.IsSuccess) : NotFound(result.IsSuccess);
    }
}