namespace FlightProvider.AirSearch.Service.Models.Responses;

public class ApiResponse<T>
{
    public T Data { get; set; }

    public int StatusCode { get; set; }

    public bool IsSuccessful { get; set; }

    public List<string> Errors { get; set; }

    // Static Factory Method
    public static ApiResponse<T> Success(T data, int statusCode)
    {
        return new ApiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
    }

    public static ApiResponse<T> Success(int statusCode)
    {
        return new ApiResponse<T> { Data = default, StatusCode = statusCode, IsSuccessful = true };
    }

    public static ApiResponse<T> Success(string message, int statusCode)
    {
        return new ApiResponse<T> { Errors = new List<string>() { message }, StatusCode = statusCode, IsSuccessful = true };
    }

    public static ApiResponse<T> Fail(string error, int statusCode)
    {
        return new ApiResponse<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
    }

    public static ApiResponse<T> Fail(List<string> errors, int statusCode)
    {
        return new ApiResponse<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
    }

    public static ApiResponse<T> Fail(T data, int statusCode)
    {
        return new ApiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = false };
    }

    public static ApiResponse<T> Fail(T data, string error, int statusCode)
    {
        return new ApiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = false, Errors = new List<string> { error } };
    }

    public static ApiResponse<T> Fail(T data, List<string> errors, int statusCode)
    {
        return new ApiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = false, Errors = errors };
    }

}
