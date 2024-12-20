using System.ComponentModel.DataAnnotations;

namespace SchoolApi.API.Dtos.Requests;

public class CreateClassDto {
    [MinLength(4,ErrorMessage = "This name require 4 - 5 characters")]
    [MaxLength(6,ErrorMessage = "This name require 4 - 5 characters")]
    public string ClassName { get; set; }
}