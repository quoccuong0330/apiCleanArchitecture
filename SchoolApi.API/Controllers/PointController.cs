using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.API.Dtos.Requests;
using SchoolApi.Application.Commands.Point;
using SchoolApi.Application.Extensions;
using SchoolApi.Application.Queries.Point;

namespace SchoolApi.API.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PointController(ISender sender) : ControllerBase {

    [HttpGet("get-point/{id}")]
    public async Task<IActionResult> GetPointById([FromRoute] Guid id) {
        var result = await sender.Send(new GetPointByIdQuery(id));
        return result.IsSuccess ? Ok(result.Data.ToDto()) : NotFound(result.Message);
    }
    
    [HttpPut("update-point/{id}")]
    public async Task<IActionResult> UpdatePointBy([FromRoute] Guid id,[FromBody] UpdatePointDto updatePointDto) {
        var result = await sender.Send(new UpdatePointCommand(id, updatePointDto.ToEntity()));
        return result.IsSuccess ? Ok(result.Data.ToDto()) : NotFound(result.Message);
    }


}