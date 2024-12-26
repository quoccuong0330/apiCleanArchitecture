namespace SchoolApi.Application.ResponseObject;

public class Result<T> {
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }
    
    private Result(bool isSuccess, T? data, string? errorMessage)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = errorMessage;
    }

    public static Result<T> Success(T data) => new Result<T>(true, data, null);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, default, errorMessage);
}